﻿using Petrovich.Web.Core.Controllers;
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

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.ProductsAdmin })]
    public partial class DataStructureController : BaseController
    {
        private readonly IDataStructureService dataStructureService;

        public DataStructureController(IDataStructureService dataStructureService, ILoggingService logger)
            : base(logger)
        {
            this.dataStructureService = dataStructureService;
        }

        [HttpGet]
        public async Task<ActionResult> BranchList()
        {
            try
            {
                var branches = await dataStructureService.ListBranchesAsync();
                var model = branches.Select(item => BranchModel.Create(item));
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
            return View(new CreateBranchModel());
        }
        
        [HttpPost]
        public async Task<ActionResult> BranchCreate(CreateBranchModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newBranch = new Branch()
                    {
                        Title = model.Title,
                        InventoryPart = model.InventoryPart,
                    };

                    await dataStructureService.CreateBranchAsync(newBranch);
                    return RedirectToAction(PetrovichRoutes.DataStructure.BranchList);
                }
            }
            catch (DuplicateBranchInventoryPartException)
            {
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
                var model = new EditBranchModel()
                {
                    BranchId = branch.BranchId,
                    Title = branch.Title,
                    InventoryPart = branch.InventoryPart,

                    Created = branch.Created,
                    CreatedBy = branch.CreatedBy,
                    Modified = branch.Modified,
                    ModifiedBy = branch.ModifiedBy,
                };
                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
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
        public async Task<ActionResult> BranchEdit(EditBranchModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var branch = new Branch()
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
            catch (BranchInventoryPartChangedException)
            {
                ModelState.AddModelError(typeof(BranchInventoryPartChangedException).Name, Properties.Resources.Branch_InventoryPart_Changed_Error);
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
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }
    }
}