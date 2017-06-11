using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Performance
{
    public sealed class PerformanceMonitor : IDisposable
    {
        public delegate void LogMethod(TimeSpan elapsed, object arguments);

        private readonly LogMethod logMethod;
        private readonly object inputArguments;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public PerformanceMonitor(LogMethod logMethod, object inputArguments = null)
        {
            this.logMethod = logMethod;
            this.inputArguments = inputArguments;

            stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            logMethod(stopwatch.Elapsed, inputArguments);
        }
    }
}
