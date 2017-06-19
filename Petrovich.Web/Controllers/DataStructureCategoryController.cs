using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using System;
using Petrovich.Business.Exceptions;
using System.Linq;
using Petrovich.Web.Models.DataStructure;
using Petrovich.Business.Models;
using Petrovich.Core.Navigation;
using System.Collections.Generic;
using Petrovich.Web.Models;

namespace Petrovich.Web.Controllers
{
    public partial class DataStructureController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> CategoryList(int page = 1)
        {
            try
            {
                var pageIndex = page - 1;
                var categories = await dataStructureService.ListCategoriesAsync(pageIndex, DefaultPageSize);
                var items = categories.Select(item => CategoryViewModel.Create(item));
                var model = new PagedListViewModel<CategoryViewModel>(items, PetrovichRoutes.DataStructure.CategoryList, page, categories.TotalCount, DefaultPageSize);
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
        public async Task<ActionResult> CategoryCreate()
        {
            try
            {
                var model = new CategoryCreateViewModel()
                {
                    Branches = await CreateBranchesSelectList(),
                };

                return View(model);
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
        public async Task<ActionResult> CategoryCreate(CategoryCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = new Category()
                    {
                        Title = model.Title,
                        BranchId = model.BranchId,
                    };

                    await dataStructureService.CreateCategoryAsync(newCategory);
                    return RedirectToAction(PetrovichRoutes.DataStructure.CategoryList);
                }

                model.Branches = await CreateBranchesSelectList();
            }
            catch (BranchNotFoundException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.CategoryCreate branch '{model.BranchId}' not found.", ex);
                ModelState.AddModelError(typeof(BranchNotFoundException).Name, Properties.Resources.Category_BranchNotFound_Error);
            }
            catch (NoBranchCategoriesSlotsException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.CategoryCreate no branch category slots available ('{model.BranchId}').", ex);
                ModelState.AddModelError(typeof(NoBranchCategoriesSlotsException).Name, Properties.Resources.Category_NoInventoryPartSlotsAvailable_Error);
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
        public async Task<ActionResult> CategoryEdit(Guid id)
        {
            try
            {
                var category = await dataStructureService.FindCategoryAsync(id);
                var model = CategoryEditViewModel.Create(category);
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
            catch (CategoryNotFoundException ex)
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
        public async Task<ActionResult> CategoryEdit(CategoryEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = new Category()
                    {
                        CategoryId = model.CategoryId,
                        Title = model.Title,
                        InventoryPart = model.InventoryPart,
                        BranchId = model.BranchId,
                    };

                    await dataStructureService.UpdateCategoryAsync(category);
                    return RedirectToAction(PetrovichRoutes.DataStructure.CategoryList);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (CategoryNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (CategoryInventoryPartChangedException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.CategoryEdit category inventory part changed ('{model.CategoryId}').", ex);
                ModelState.AddModelError(typeof(CategoryInventoryPartChangedException).Name, Properties.Resources.Category_InventoryPart_Changed_Error);
            }
            catch (BranchNotFoundException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.CategoryEdit branch '{model.BranchId}' not found.", ex);
                ModelState.AddModelError(typeof(BranchNotFoundException).Name, Properties.Resources.Category_BranchNotFound_Error);
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
        public async Task<ActionResult> CategoryDelete(Guid id)
        {
            try
            {
                await dataStructureService.DeleteCategoryAsync(id);
                return RedirectToAction(PetrovichRoutes.DataStructure.CategoryList);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (CategoryNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (ChildGroupsExistsException)
            {
                return RedirectToAction(PetrovichRoutes.DataStructure.CategoryChildGroupsExists);
            }
            catch (ChildProductsExistsException)
            {
                return RedirectToAction(PetrovichRoutes.DataStructure.CategoryChildProductsExists);
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
        public ActionResult CategoryChildProductsExists()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChildGroupsExists()
        {
            return View();
        }

        private async Task<List<SelectListItem>> CreateBranchesSelectList()
        {
            var branches = await dataStructureService.ListAllBranchesAsync();
            return branches.Select(item => new SelectListItem() { Text = item.Title, Value = item.BranchId.ToString() }).ToList();
        }
    }
}