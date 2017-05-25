using Petrovich.Business.Logging;
using Petrovich.Web.Core.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        protected readonly ILoggingService logger;

        public DashboardController(ILoggingService logger)
        {
            this.logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                await logger.LogAsync(Business.Models.Enumerations.LogSeverity.None, "Test");
                await logger.LogCriticalAsync("Critical log");
                await logger.LogCriticalAsync(new Exception("Critical exception"));
                await logger.LogCriticalAsync("Critical message", new Exception("With exception"));
                await logger.LogCriticalAsync("Critical message", new Exception("With exception", new Exception("With inner exception")));
                await logger.LogErrorAsync("Error log");
                await logger.LogErrorAsync(new Exception("Error exception"));
                await logger.LogErrorAsync("Error message", new Exception("With exception"));
                await logger.LogErrorAsync("Error message", new Exception("With exception", new Exception("With inner exception")));
                await logger.LogInformationAsync("Information log");
                await logger.LogInformationAsync(new Exception("Information exception"));
                await logger.LogInformationAsync("Information message", new Exception("With exception"));
                await logger.LogInformationAsync("Information message", new Exception("With exception", new Exception("With inner exception")));
                await logger.LogNoneAsync("None log");
            }
            catch(Exception ex)
            {
                var i = 0;
            }

            return View();
        }
    }
}