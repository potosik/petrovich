using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Business;
using Petrovich.Web.Models;
using Petrovich.Business.Exceptions;
using Petrovich.Web.Models.Search;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [Authorize]
    public class SearchController : BaseController
    {
        private const int MaxFastResultsCount = 10;

        private readonly IProductService productService;

        public SearchController(IProductService productService, ILoggingService loggingService) 
            : base(loggingService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<JsonResult> Fast(string q)
        {
            try
            {
                var items = await productService.SearchFastAsync(q, MaxFastResultsCount);
                var products = items.Select(item => ProductFastViewModel.Create(item));
                return JsonAllowGet(new JsonResponse(result: products));
            }
            catch (Exception ex)
            {
                var errorMessage = JsonResponse.FormatExceptionMessage(ex);
                await logger.LogErrorAsync(errorMessage);
                return JsonAllowGet(new JsonResponse(errorMessage: errorMessage));
            }
        }
    }
}