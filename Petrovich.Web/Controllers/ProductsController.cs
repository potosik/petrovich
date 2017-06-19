using Petrovich.Web.Core.Controllers;
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

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.ProductsAdmin })]
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
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                var pageIndex = page - 1;
                var products = await productService.ListAsync(pageIndex, DefaultPageSize);
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
        public async Task<ActionResult> Create()
        {
            try
            {
                var model = new ProductCreateViewModel()
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
        public async Task<ActionResult> Create(ProductCreateViewModel model, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = file.GetImage();
                    var newProduct = new Product()
                    {
                        Title = model.Title,
                        Description = model.Description,

                        ImageFull = image.GetFullImageString(),
                        ImageDefault = image.GetDefaultImageString(),
                        ImageSmall = image.GetSmallImageString(),

                        CategoryId = model.CategoryId,
                        GroupId = model.GroupId,
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
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var product = await productService.FindAsync(id);
                var model = ProductEditViewModel.Create(product);
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
        public async Task<ActionResult> Edit(ProductEditViewModel model, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = file.GetImage();
                    var product = new Product()
                    {
                        ProductId = model.ProductId,
                        Title = model.Title,
                        Description = model.Description,
                        InventoryPart = model.InventoryPart,

                        ImageFull = image.GetFullImageString() ?? model.ImageFull,
                        ImageDefault = image.GetDefaultImageString() ?? model.ImageDefault,
                        ImageSmall = image.GetSmallImageString() ?? model.ImageSmall,

                        CategoryId = model.CategoryId,
                        GroupId = model.GroupId,
                    };

                    await productService.UpdateAsync(product);
                    return RedirectToAction(PetrovichRoutes.Products.Index);
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
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await productService.DeleteAsync(id);
                return RedirectToAction(PetrovichRoutes.Products.Index);
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
                return JsonAllowGet(new JsonResponse(result: categories));
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

            return JsonAllowGet(new JsonResponse(result: new List<SelectListItem>()));
        }

        [HttpGet]
        public async Task<JsonResult> GetGroups(Guid categoryId)
        {
            try
            {
                var groups = await CreateGroupsSelectList(categoryId);
                return JsonAllowGet(new JsonResponse(result: groups));
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

            return JsonAllowGet(new JsonResponse(result: new List<SelectListItem>()));
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