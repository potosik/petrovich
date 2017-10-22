using Petrovich.Core;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Web.Models.Bid;
using Newtonsoft.Json;
using Petrovich.Web.Core.DTOs;
using Petrovich.Business;
using Petrovich.Business.Exceptions;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.Manager, PermissionClaims.PowerAdmin })]
    public class BidController : BaseController
    {
        private readonly IProductService productService;

        public BidController(IProductService productService, ILoggingService logger) 
            : base(logger)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string serializedList)
        {
            try
            {
                var deserializedResult = JsonConvert.DeserializeObject<List<BidProductItemDTO>>(serializedList);
                var selectedProducts = await productService.ListAsync(deserializedResult.Select(item => item.ProductId).Distinct());
                var products = selectedProducts.Select(item => BidProductViewModel.Create(item)).ToList();
                var model = new CreateBidViewModel()
                {
                    Products = products,
                    PriceTypes = CreatePriceTypeSelectList(),
                };
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
        public async Task<ActionResult> Create(CreateBidViewModel model)
        {
            try
            {
                var i = 0;
                return await CreateBadRequestResponseAsync(new Exception());
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