using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Core;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Web.Models;
using Petrovich.Web.Models.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.PowerAdmin })]
    public class LoggingController : BaseController
    {
        public LoggingController(ILoggingService logging)
            : base(logging)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                var pageIndex = page - 1;
                var logs = await logger.ListAsync(pageIndex, DefaultPageSize);
                var items = logs.Select(item => LogViewModel.Create(item));
                var model = new PagedListViewModel<LogViewModel>(items, PetrovichRoutes.Logging.Index, page, logs.TotalCount, DefaultPageSize);
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
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var log = await logger.FindAsync(id);
                return View(LogViewModel.Create(log));
            }
            catch (ArgumentNullException ex)
            {
                return await CreateBadRequestResponseAsync(ex);
            }
            catch (LogNotFoundException ex)
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