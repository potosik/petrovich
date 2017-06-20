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
using Petrovich.Core.Extensions;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [Authorize]
    public class ImagesController : BaseController
    {
        private readonly IFullImageService fullImageService;

        public ImagesController(IFullImageService fullImageService, ILoggingService logger) 
            : base(logger)
        {
            this.fullImageService = fullImageService;
        }

        [LayoutInjecter("_LayoutEmptyWhite")]
        public async Task<ActionResult> Index(Guid id)
        {
            try
            {
                var image = await fullImageService.FindAsync(id);
                return View("FullSize", model: image.ToBase64String());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (FullImageNotFoundException ex)
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