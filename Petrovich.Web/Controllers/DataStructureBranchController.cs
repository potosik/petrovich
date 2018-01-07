using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Core;
using System.Threading.Tasks;
using Petrovich.Business;
using Petrovich.Web.Models.DataStructure;
using System.Linq;
using Petrovich.Business.Models;
using System;
using Petrovich.Core.Navigation;
using Petrovich.Business.Exceptions;
using Petrovich.Web.Models;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.ProductsAdmin, PermissionClaims.PowerAdmin })]
    public partial class DataStructureController : BaseController
    {
        private readonly IDataStructureService dataStructureService;

        public DataStructureController(IDataStructureService dataStructureService, ILoggingService logger)
            : base(logger)
        {
            this.dataStructureService = dataStructureService;
        }

        [HttpGet]
        public async Task<ActionResult> BranchList(int page = 1)
        {
            try
            {
                var pageIndex = page - 1;
                var branches = await dataStructureService.ListBranchesAsync(pageIndex, DefaultPageSize);
                var items = branches.Select(item => BranchViewModel.Create(item));
                var model = new PagedListViewModel<BranchViewModel>(items, PetrovichRoutes.DataStructure.BranchList, page, branches.TotalCount, DefaultPageSize);
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
        public ActionResult BranchCreate()
        {
            return View(new BranchCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BranchCreate(BranchCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newBranch = new BranchModel()
                    {
                        Title = model.Title,
                        InventoryPart = model.InventoryPart,
                    };

                    await dataStructureService.CreateBranchAsync(newBranch);
                    return RedirectToAction(PetrovichRoutes.DataStructure.BranchList);
                }
            }
            catch (DuplicateBranchInventoryPartException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.BranchCreate duplicate branch inventory part found.", ex);
                ModelState.AddModelError(typeof(DuplicateBranchInventoryPartException).Name, Properties.Resources.Branch_InventoryPart_Duplicate_Error);
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

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> BranchEdit(Guid id)
        {
            try
            {
                var branch = await dataStructureService.FindBranchAsync(id);
                var model = BranchEditViewModel.Create(branch);
                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (BranchNotFoundException ex)
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
        public async Task<ActionResult> BranchEdit(BranchEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var branch = new BranchModel()
                    {
                        BranchId = model.BranchId,
                        Title = model.Title,
                        InventoryPart = model.InventoryPart,
                    };

                    await dataStructureService.UpdateBranchAsync(branch);
                    return RedirectToAction(PetrovichRoutes.DataStructure.BranchList);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (BranchNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (BranchInventoryPartChangedException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.BranchEdit branch '{model.BranchId}' inventory part changed.", ex);
                ModelState.AddModelError(typeof(BranchInventoryPartChangedException).Name, Properties.Resources.Branch_InventoryPart_Changed_Error);
            }
            catch (DuplicateBranchInventoryPartException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.BranchEdit branch '{model.BranchId}' duplicate inventory found.", ex);
                ModelState.AddModelError(typeof(DuplicateBranchInventoryPartException).Name, Properties.Resources.Branch_InventoryPart_Duplicate_Error);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BranchDelete(Guid id)
        {
            try
            {
                await dataStructureService.DeleteBranchAsync(id);
                return RedirectToAction(PetrovichRoutes.DataStructure.BranchList);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (BranchNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (ChildCategoriesExistsException)
            {
                return RedirectToAction(PetrovichRoutes.DataStructure.BranchChildCategoriesExists);
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
        public ActionResult ChildCategoriesExists()
        {
            return View();
        }
    }
}