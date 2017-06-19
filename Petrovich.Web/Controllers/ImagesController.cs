using Petrovich.Web.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Business.Exceptions;
using Petrovich.Business;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [Authorize]
    public class ImagesController : BaseController
    {
        private readonly IProductService productService;

        public ImagesController(IProductService productService, ILoggingService logger) 
            : base(logger)
        {
            this.productService = productService;
        }

        [LayoutInjecter("_LayoutEmptyWhite")]
        public async Task<ActionResult> Product(Guid id)
        {
            try
            {
                var image = await productService.FindImageAsync(id);
                return View("FullSize", model: image);
            }
            catch (ImageNotFoundException ex)
            {
                return await CreateNotFoundResponseAsync(ex);
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
    }
}