using Advanced_API_BSDi_csharp_BDD.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Utilities
{
    public class SerilogProvider : ILogProvider
    {
        private readonly ILogger _logger;
        public SerilogProvider()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public void Info(string message) => _logger.Information(message);
        public void Error(string message) => _logger.Error(message);
        public void Step(string message) => _logger.Information("[STEP] " + message);
    }
}
