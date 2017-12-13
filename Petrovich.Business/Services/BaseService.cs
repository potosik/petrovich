using Petrovich.Business.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Services
{
    public class BaseService
    {
        protected readonly ILoggingService logger;

        public BaseService(ILoggingService loggingService)
        {
            this.logger = loggingService;
        }
    }
}
