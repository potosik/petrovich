using Petrovich.Business;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Core;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Web.Models;
using Petrovich.Web.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.Manager, PermissionClaims.DataStructureAdmin, PermissionClaims.ProductsAdmin, PermissionClaims.PowerAdmin })]
    public class ManagerController : BaseController
    {
        private readonly IDataStructureService dataStructureService;
        private readonly IProductService productService;

        public ManagerController(IDataStructureService dataStructureService, IProductService productService, ILoggingService logger)
            : base(logger)
        {
            this.productService = productService;
            this.dataStructureService = dataStructureService;
        }

        [HttpGet]
        public async Task<ActionResult> Branches()
        {
            try
            {
                var branches = await dataStructureService.ListAllBranchesAsync();
                var model = branches.Select(item => BranchViewModel.Create(item)).OrderBy(item => item.Title);
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
        public async Task<ActionResult> Categories(Guid branchId)
        {
            try
            {
                var branch = await dataStructureService.FindBranchAsync(branchId);
                var categories = await dataStructureService.ListCategoriesByBranchIdAsync(branch.BranchId);
                var model = CategoriesViewModel.Create(branch, categories);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
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

        [HttpGet]
        public async Task<ActionResult> Groups(Guid branchId, Guid categoryId)
        {
            try
            {
                var branch = await dataStructureService.FindBranchAsync(branchId);
                var category = await dataStructureService.FindCategoryAsync(categoryId);
                var groups = await dataStructureService.ListGroupsByCategoryIdAsync(category.CategoryId);
                var products = await productService.ListByCategoryIdAsync(category.CategoryId);
                var model = GroupsViewModel.Create(branch, category, groups, products);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (BranchNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
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

        [HttpGet]
        public async Task<ActionResult> Products(Guid branchId, Guid categoryId, Guid groupId)
        {
            try
            {
                var branch = await dataStructureService.FindBranchAsync(branchId);
                var category = await dataStructureService.FindCategoryAsync(categoryId);
                var group = await dataStructureService.FindGroupAsync(groupId);
                var products = await productService.ListByGroupIdAsync(group.GroupId);
                var model = ProductsViewModel.Create(branch, category, group, products);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (BranchNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (CategoryNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
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

        [HttpGet]
        public async Task<ActionResult> ProductDetails(Guid id)
        {
            try
            {
                var product = await productService.FindAsync(id);
                var model = ProductDetailsViewModel.Create(product);
                return View(model);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ProductNotFoundException ex)
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