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

namespace Petrovich.Web.Controllers
{
    public partial class DataStructureController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GroupList()
        {
            try
            {
                var groups = await dataStructureService.ListGroupsAsync();
                var model = groups.Select(item => GroupViewModel.Create(item));
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
        public async Task<ActionResult> GroupCreate(GroupCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGroup = new Group()
                    {
                        Title = model.Title,
                        CategoryId = model.CategoryId,
                    };

                    await dataStructureService.CreateGroupAsync(newGroup);
                    return RedirectToAction(PetrovichRoutes.DataStructure.GroupList);
                }

                model.Categories = await CreateGroupsSelectList();
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
                var model = new GroupEditViewModel()
                {
                    GroupId = group.GroupId,
                    Title = group.Title,
                    CategoryId = group.CategoryId,

                    CategoryTitle = group.CategoryTitle,

                    Created = group.Created,
                    CreatedBy = group.CreatedBy,
                    Modified = group.Modified,
                    ModifiedBy = group.ModifiedBy,
                };
                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
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

            return View(model);
        }

        [HttpPost]
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
            catch (DatabaseOperationException ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
            catch (Exception ex)
            {
                return await CreateInternalServerErrorResponseAsync(ex);
            }
        }

        private async Task<List<SelectListItem>> CreateGroupsSelectList()
        {
            var branches = await dataStructureService.ListCategoriesAsync();
            return branches.Select(item => new SelectListItem() { Text = item.Title, Value = item.CategoryId.ToString() }).ToList();
        }
    }
}