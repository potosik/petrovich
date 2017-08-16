using Petrovich.Business;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Core;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
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
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.Manager, PermissionClaims.ProductsAdmin, PermissionClaims.PowerAdmin })]
    public class ManagerController : BaseController
    {
        private readonly IProductService productService;

        public ManagerController(IProductService productService, ILoggingService logger)
            : base(logger)
        {
            this.productService = productService;
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