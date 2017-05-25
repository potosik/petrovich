using Petrovich.Business.Logging;
using Petrovich.Web.Core.Controllers;
using System;
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

        public ActionResult Index()
        {
            try
            {
                logger.LogAsync(Business.Models.Enumerations.LogSeverity.None, "Test");
                logger.LogCriticalAsync("Critical log");
                logger.LogCriticalAsync(new Exception("Critical exception"));
                logger.LogCriticalAsync("Critical message", new Exception("With exception"));
                logger.LogCriticalAsync("Critical message", new Exception("With exception", new Exception("With inner exception")));
                logger.LogErrorAsync("Error log");
                logger.LogErrorAsync(new Exception("Error exception"));
                logger.LogErrorAsync("Error message", new Exception("With exception"));
                logger.LogErrorAsync("Error message", new Exception("With exception", new Exception("With inner exception")));
                logger.LogInformationAsync("Information log");
                logger.LogInformationAsync(new Exception("Information exception"));
                logger.LogInformationAsync("Information message", new Exception("With exception"));
                logger.LogInformationAsync("Information message", new Exception("With exception", new Exception("With inner exception")));
                logger.LogNoneAsync("None log");
            }
            catch(Exception ex)
            {
                var i = 0;
            }

            return View();
        }
    }
}