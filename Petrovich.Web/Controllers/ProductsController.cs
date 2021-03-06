﻿using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Core;
using System.Threading.Tasks;
using Petrovich.Business;
using System.Linq;
using Petrovich.Web.Models.Products;
using System;
using Petrovich.Business.Exceptions;
using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Core.Navigation;
using Petrovich.Web.Models;
using System.Web;
using System.Drawing;
using Petrovich.Web.Core.Extensions;
using Petrovich.Core.Extensions;
using Petrovich.Business.Models.Enumerations;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.ProductsAdmin, PermissionClaims.PowerAdmin })]
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;
        private readonly IDataStructureService dataStructureService;

        public ProductsController(IProductService productService, IDataStructureService dataStructureService, ILoggingService logger)
            : base(logger)
        {
            this.productService = productService;
            this.dataStructureService = dataStructureService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string filter, int page = 1)
        {
            try
            {
                ViewBag.QueryFilter = filter;

                var pageIndex = page - 1;
                var products = await productService.ListAsync(filter, pageIndex, DefaultPageSize);
                var items = products.Select(item => ProductViewModel.Create(item));
                var model = new PagedListViewModel<ProductViewModel>(items, PetrovichRoutes.Products.Index, page, products.TotalCount, DefaultPageSize);
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
        public async Task<ActionResult> Create(Guid? source = null)
        {
            try
            {
                var model = new ProductCreateViewModel()
                {
                    Branches = await CreateBranchesSelectList(),
                };

                if (source.HasValue)
                {
                    var sourceProduct = await productService.FindAsync(source.Value);
                    model.Title = sourceProduct.Title;
                    model.Description = sourceProduct.Description;
                    model.Defects = sourceProduct.Defects;
                    model.Price = sourceProduct.Price;
                    model.AssessedValue = sourceProduct.AssessedValue;
                    model.PurchaseYear = sourceProduct.PurchaseYear;
                    model.PurchaseMonth = sourceProduct.PurchaseMonth;

                    model.BranchId = sourceProduct.BranchId;

                    model.CategoryId = sourceProduct.Category.CategoryId;
                    model.Categories = await CreateCategoriesSelectList(sourceProduct.BranchId);

                    model.GroupId = sourceProduct.Group?.GroupId;
                    model.Groups = await CreateGroupsSelectList(sourceProduct.Category.CategoryId);
                }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateViewModel model, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = file.GetImage();
                    var newProduct = new ProductModel()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Defects = model.Defects,
                        Price = model.Price,
                        AssessedValue = model.AssessedValue,

                        PurchaseYear = model.PurchaseYear,
                        PurchaseMonth = model.PurchaseMonth,

                        ImageFull = image.GetFullImageByteArray(),
                        ImageDefault = image.GetDefaultImageString(),
                        ImageSmall = image.GetSmallImageString(),

                        Category = new CategoryModel() { CategoryId = model.CategoryId },
                        Group = model.GroupId.HasValue ? new GroupModel() { GroupId = model.GroupId.Value } : null,
                    };

                    await productService.CreateAsync(newProduct);
                    return RedirectToAction(PetrovichRoutes.Products.Index);
                }

                model.Branches = await CreateBranchesSelectList();
                model.Categories = await CreateCategoriesSelectList(model.BranchId);
                model.Groups = await CreateGroupsSelectList(model.CategoryId);
            }
            catch (InvalidImageFormatException ex)
            {
                await logger.LogInformationAsync($"ProductsController.Create invalid image format.", ex);
                ModelState.AddModelError(typeof(InvalidImageFormatException).Name, Properties.Resources.Image_InvalidFormat_Error);
            }
            catch (BranchNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductController.Create branch '{model.BranchId}' not found.", ex);
                ModelState.AddModelError(typeof(BranchNotFoundException).Name, Properties.Resources.Product_BranchNotFound_Error);
            }
            catch (CategoryNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductController.Create category '{model.CategoryId}' not found.", ex);
                ModelState.AddModelError(typeof(CategoryNotFoundException).Name, Properties.Resources.Product_CategoryNotFound_Error);
            }
            catch (GroupNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductController.Create group '{model.GroupId}' not found.", ex);
                ModelState.AddModelError(typeof(GroupNotFoundException).Name, Properties.Resources.Product_GroupNotFound_Error);
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
        public async Task<ActionResult> Edit(Guid id, string returnUrl = null)
        {
            try
            {
                var product = await productService.FindAsync(id);
                return View(ProductEditViewModel.Create(product));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (ArgumentNullException ex)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductEditViewModel model, HttpPostedFileBase file, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = file.GetImage();
                    var product = new ProductModel()
                    {
                        ProductId = model.ProductId,
                        Title = model.Title,
                        Description = model.Description,
                        Defects = model.Defects,
                        Price = model.Price,
                        AssessedValue = model.AssessedValue,
                        InventoryPart = model.InventoryPart,

                        PurchaseYear = model.PurchaseYear,
                        PurchaseMonth = model.PurchaseMonth,

                        ImageFull = image.GetFullImageByteArray() ?? model.ImageFull.FromBase64String(),
                        ImageDefault = image.GetDefaultImageString() ?? model.ImageDefault,
                        ImageSmall = image.GetSmallImageString() ?? model.ImageSmall,

                        Category = new CategoryModel() { CategoryId = model.CategoryId },
                        Group = model.GroupId.HasValue ? new GroupModel() { GroupId = model.GroupId.Value } : null,
                    };

                    await productService.UpdateAsync(product);
                    return RedirectToLocalOrAction(returnUrl, PetrovichRoutes.Products.Index);
                }
            }
            catch (InvalidImageFormatException ex)
            {
                await logger.LogInformationAsync($"ProductsController.Edit invalid image format.", ex);
                ModelState.AddModelError(typeof(InvalidImageFormatException).Name, Properties.Resources.Image_InvalidFormat_Error);
            }
            catch (ProductNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
            }
            catch (CategoryNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductsController.Edit category '{model.CategoryId}' not found.", ex);
                ModelState.AddModelError(typeof(CategoryNotFoundException).Name, Properties.Resources.Product_CategoryNotFound_Error);
            }
            catch (GroupNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductsController.Edit group '{model.GroupId}' not found.", ex);
                ModelState.AddModelError(typeof(GroupNotFoundException).Name, Properties.Resources.Product_GroupNotFound_Error);
            }
            catch (ProductInventoryPartChangedException ex)
            {
                await logger.LogInformationAsync($"ProductsController.Edit product inventory part is changed ({model.ProductId}).", ex);
                ModelState.AddModelError(typeof(ProductInventoryPartChangedException).Name, Properties.Resources.Product_InventoryChanged_Error);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, string returnUrl = null)
        {
            try
            {
                await productService.DeleteAsync(id);
                return RedirectToLocalOrAction(returnUrl, PetrovichRoutes.Products.Index);
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

        [HttpGet]
        public async Task<JsonResult> GetCategories(Guid branchId)
        {
            try
            {
                var categories = await CreateCategoriesSelectList(branchId);
                return JsonAllowGet(new JsonResponseViewModel(result: categories));
            }
            catch (BranchNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories branch '{branchId}' not found.", ex);
            }
            catch (ArgumentNullException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories argument null exception thrown.", ex);
            }
            catch (DatabaseOperationException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories database operation exception thrown.", ex);
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync($"ProductController.GetCategories unexpected exception occured.", ex);
            }

            return JsonAllowGet(new JsonResponseViewModel(result: new List<SelectListItem>()));
        }

        [HttpGet]
        public async Task<JsonResult> GetGroups(Guid categoryId)
        {
            try
            {
                var groups = await CreateGroupsSelectList(categoryId);
                return JsonAllowGet(new JsonResponseViewModel(result: groups));
            }
            catch (CategoryNotFoundException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories category '{categoryId}' not found.", ex);
            }
            catch (ArgumentNullException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories argument null exception thrown.", ex);
            }
            catch (DatabaseOperationException ex)
            {
                await logger.LogInformationAsync($"ProductController.GetCategories database operation exception thrown.", ex);
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync($"ProductController.GetCategories unexpected exception occured.", ex);
            }

            return JsonAllowGet(new JsonResponseViewModel(result: new List<SelectListItem>()));
        }

        private async Task<IList<SelectListItem>> CreateBranchesSelectList()
        {
            var branches = await dataStructureService.ListAllBranchesAsync();
            return branches.Select(item => new SelectListItem() { Text = item.Title, Value = item.BranchId.ToString() }).ToList();
        }

        private async Task<IList<SelectListItem>> CreateCategoriesSelectList(Guid branchId)
        {
            if (branchId == Guid.Empty)
            {
                return new List<SelectListItem>();
            }

            var categories = await dataStructureService.ListCategoriesByBranchIdAsync(branchId);
            return categories.Select(item => new SelectListItem() { Text = item.Title, Value = item.CategoryId.ToString() }).ToList();
        }

        private async Task<IList<SelectListItem>> CreateGroupsSelectList(Guid? categoryId)
        {
            if (!categoryId.HasValue || categoryId.Value == Guid.Empty)
            {
                return new List<SelectListItem>();
            }

            var groups = await dataStructureService.ListGroupsByCategoryIdAsync(categoryId.Value);
            return groups.Select(item => new SelectListItem() { Text = item.Title, Value = item.GroupId.ToString() }).ToList();
        }
    }
}