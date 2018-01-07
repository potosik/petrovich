using Petrovich.Business;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Business.Models;
using Petrovich.Core;
using Petrovich.Core.Extensions;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Extensions;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Web.Models;
using Petrovich.Web.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.PowerAdmin, PermissionClaims.Manager })]
    public class ClientsController : BaseController
    {
        private const string DefaultPickParameter = "clientId";

        private readonly IClientService clientService;

        public ClientsController(IClientService clientService, ILoggingService loggingService)
            : base(loggingService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string filter, int page = 1)
        {
            try
            {
                ViewBag.QueryFilter = filter;

                var pageIndex = page - 1;
                var clients = await clientService.ListAsync(filter, pageIndex, DefaultPageSize);
                var items = clients.Select(item => ClientViewModel.Create(item));
                var model = new PagedListViewModel<ClientViewModel>(items, PetrovichRoutes.Clients.Index, page, clients.TotalCount, DefaultPageSize);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                return View(new ClientCreateViewModel());
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientCreateViewModel model, string pickReturnUrl, string parameter = DefaultPickParameter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newClient = new ClientModel()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        Registered = model.Registered,
                        PassportId = model.PassportId,
                        PassportData = model.PassportData,
                        PersonalId = model.PersonalId,
                        BirthDate = model.BirthDate,
                        PhonesJson = model.PhonesJson,
                    };

                    var client = await clientService.CreateAsync(newClient);
                    if (String.IsNullOrWhiteSpace(pickReturnUrl))
                    {
                        return RedirectToAction(PetrovichRoutes.Clients.Index);
                    }

                    var returnUri = PrepareReturnUrl(pickReturnUrl);
                    return Redirect(returnUri.SetParameter(parameter, client.ClientId.ToString()).ToString());
                }
            }

            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (ClientPassportIdAlreadyExistException ex)
            {
                await logger.LogInformationAsync($"ClientController.Create client with password id '{model.PassportId}' already exist.", ex);
                ModelState.AddModelError(typeof(ClientPassportIdAlreadyExistException).Name, Properties.Resources.Client_Passport_Id_Already_Exist_Error);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var client = await clientService.FindAsync(id);
                return View(ClientEditViewModel.Create(client));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ClientNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientEditViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new ClientModel()
                    {
                        ClientId = model.ClientId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        Registered = model.Registered,
                        PassportId = model.PassportId,
                        PassportData = model.PassportData,
                        PersonalId = model.PersonalId,
                        BirthDate = model.BirthDate,
                        PhonesJson = model.PhonesJson,
                    };

                    await clientService.UpdateAsync(client);
                    return RedirectToLocalOrAction(returnUrl, PetrovichRoutes.Clients.Index);
                }
            }
            catch (ClientNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (ClientPassportIdAlreadyExistException ex)
            {
                await logger.LogInformationAsync($"ClientController.Edit client with password id '{model.PassportId}' already exist.", ex);
                ModelState.AddModelError(typeof(ClientPassportIdAlreadyExistException).Name, Properties.Resources.Client_Passport_Id_Already_Exist_Error);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Pick(string filter, string returnUrl, int page = 1, string parameter = DefaultPickParameter)
        {
            try
            {
                ViewBag.QueryFilter = filter;
                ViewBag.PickReturnUrl = returnUrl;
                ViewBag.Parameter = parameter;

                var pageIndex = page - 1;
                var returnUri = PrepareReturnUrl(returnUrl);
                var clients = await clientService.ListAsync(filter, pageIndex, DefaultPageSize);
                var items = clients.Select(item => ClientPickViewModel.Create(item, returnUri, parameter));
                var model = new PagedListViewModel<ClientPickViewModel>(items, PetrovichRoutes.Clients.Index, page, clients.TotalCount, DefaultPageSize);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        private Uri PrepareReturnUrl(string returnUrl)
        {
            returnUrl = String.IsNullOrWhiteSpace(returnUrl) ? Url.Action(PetrovichRoutes.Clients.Index) : returnUrl;
            returnUrl = returnUrl.StartsWith("/") ? returnUrl : $"/{returnUrl}";

            var uri = new Uri(returnUrl, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return uri;
            }

            returnUrl = VirtualPathUtility.ToAbsolute(returnUrl);
            return new Uri(Request.Url, returnUrl);
        }
    }
}