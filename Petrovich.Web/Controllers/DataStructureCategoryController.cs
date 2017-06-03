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

namespace Petrovich.Web.Controllers
{
    public partial class DataStructureController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> CategoryList()
        {
            try
            {
                var categories = await dataStructureService.ListCategoriesAsync();
                var model = categories.Select(item => CategoryModel.Create(item));
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
            var model = new CreateCategoryModel()
            {
                Branches = await CreateBranchesSelectList(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CategoryCreate(CreateCategoryModel model)
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
            }
            catch (DuplicateCategoryInventoryPartException)
            {
                ModelState.AddModelError(typeof(DuplicateCategoryInventoryPartException).Name, Properties.Resources.Category_InventoryPart_Duplicate_Error);
            }
            catch (BranchNotFoundException)
            {
                ModelState.AddModelError(typeof(BranchNotFoundException).Name, Properties.Resources.Category_BranchNotFound_Error);
            }
            catch (NoBranchCategoriesSlotsException)
            {
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

            model.Branches = await CreateBranchesSelectList();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> CategoryEdit(Guid id)
        {
            try
            {
                var category = await dataStructureService.FindCategoryAsync(id);
                var model = new EditCategoryModel()
                {
                    CategoryId = category.CategoryId,
                    Title = category.Title,
                    InventoryPart = category.InventoryPart,
                    BranchId = category.BranchId,

                    BranchTitle = category.BranchTitle,

                    Created = category.Created,
                    CreatedBy = category.CreatedBy,
                    Modified = category.Modified,
                    ModifiedBy = category.ModifiedBy,
                };
                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
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
        public async Task<ActionResult> CategoryEdit(EditCategoryModel model)
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
            catch (CategoryInventoryPartChangedException)
            {
                ModelState.AddModelError(typeof(CategoryInventoryPartChangedException).Name, Properties.Resources.Category_InventoryPart_Changed_Error);
            }
            catch (BranchNotFoundException)
            {
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
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        private async Task<List<SelectListItem>> CreateBranchesSelectList()
        {
            var branches = await dataStructureService.ListBranchesAsync();
            return branches.Select(item => new SelectListItem() { Text = item.Title, Value = item.BranchId.ToString() }).ToList();
        }
    }
}