using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Petrovich.Business.Exceptions;
using Petrovich.Web.Models.DataStructure;
using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Core.Navigation;
using Petrovich.Web.Models;
using Petrovich.Business.Models.Enumerations;

namespace Petrovich.Web.Controllers
{
    public partial class DataStructureController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GroupList(int page = 1)
        {
            try
            {
                var pageIndex = page - 1;
                var groups = await dataStructureService.ListGroupsAsync(pageIndex, DefaultPageSize);
                var items = groups.Select(item => GroupViewModel.Create(item));
                var model = new PagedListViewModel<GroupViewModel>(items, PetrovichRoutes.DataStructure.GroupList, page, groups.TotalCount, DefaultPageSize);
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
        public async Task<ActionResult> GroupCreate()
        {
            try
            {
                var model = new GroupCreateViewModel()
                {
                    Categories = await CreateGroupsSelectList(),
                    PriceTypes = CreatePriceTypeSelectList(),
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
        public async Task<ActionResult> GroupCreate(GroupCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGroup = new Group()
                    {
                        Title = model.Title,
                        BasePrice = model.BasePrice,
                        PriceType = (PriceType?)model.PriceType,
                        CategoryId = model.CategoryId,
                    };

                    await dataStructureService.CreateGroupAsync(newGroup);
                    return RedirectToAction(PetrovichRoutes.DataStructure.GroupList);
                }

                model.Categories = await CreateGroupsSelectList();
                model.PriceTypes = CreatePriceTypeSelectList();
            }
            catch (CategoryNotFoundException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.GroupCreate category '{model.CategoryId}' not found.", ex);
                ModelState.AddModelError(typeof(BranchNotFoundException).Name, Properties.Resources.Group_CategoryNotFound_Error);
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
        public async Task<ActionResult> GroupEdit(Guid id)
        {
            try
            {
                var group = await dataStructureService.FindGroupAsync(id);
                var model = GroupEditViewModel.Create(group);
                model.PriceTypes = CreatePriceTypeSelectList();
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
            catch (GroupNotFoundException ex)
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
        public async Task<ActionResult> GroupEdit(GroupEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var group = new Group()
                    {
                        GroupId = model.GroupId,
                        Title = model.Title,
                        BasePrice = model.BasePrice,
                        PriceType = (PriceType?)model.PriceType,
                        CategoryId = model.CategoryId,
                    };

                    await dataStructureService.UpdateGroupAsync(group);
                    return RedirectToAction(PetrovichRoutes.DataStructure.GroupList);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (CategoryNotFoundException ex)
            {
                await logger.LogInformationAsync($"DataStructureController.GroupEdit category '{model.CategoryId}' not found.", ex);
                ModelState.AddModelError(typeof(CategoryNotFoundException).Name, Properties.Resources.Group_CategoryNotFound_Error);
            }
            catch (GroupNotFoundException ex)
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

            model.PriceTypes = CreatePriceTypeSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GroupDelete(Guid id)
        {
            try
            {
                await dataStructureService.DeleteGroupAsync(id);
                return RedirectToAction(PetrovichRoutes.DataStructure.GroupList);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (GroupNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (ChildProductsExistsException)
            {
                return RedirectToAction(PetrovichRoutes.DataStructure.GroupChildProductsExists);
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
        public ActionResult GroupChildProductsExists()
        {
            return View();
        }

        private async Task<List<SelectListItem>> CreateGroupsSelectList()
        {
            try
            {
                var branches = await dataStructureService.ListAllCategoriesAsync();
                return branches.Select(item => new SelectListItem() { Text = item.Title, Value = item.CategoryId.ToString() }).ToList();
            }
            catch (DatabaseOperationException ex)
            {
                await logger.LogErrorAsync(ex);
            }
            catch (Exception ex)
            {
                await logger.LogCriticalAsync(ex);
            }

            return new List<SelectListItem>();
        }
    }
}