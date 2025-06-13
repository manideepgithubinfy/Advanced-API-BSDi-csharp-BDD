using Advanced_API_BSDi_csharp_BDD.Helpers;
using Advanced_API_BSDi_csharp_BDD.Interfaces;
using Advanced_API_BSDi_csharp_BDD.Utilities;
using AventStack.ExtentReports.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Reqnroll.Assist;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Advanced_API_BSDi_csharp_BDD.Hooks
{
    [Binding]
    public class TestHooks
    {
        private static IServiceProvider? _services; // Changed visibility to private
        private static ILogProvider? _logger; // Changed visibility to private
        private static IReportManager? _reportManager; // Changed visibility to private
        private readonly ScenarioContext _context;

        public TestHooks(ScenarioContext context) => _context = context;

        [BeforeTestRun]
        public static void GlobalSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<ILogProvider, SerilogProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddSingleton<IEnvironmentHelper, EnvironmentHelper>();
            services.AddSingleton<IReportManager, ExtentReportManager>();

            _services = services.BuildServiceProvider();
            _logger = _services.GetService<ILogProvider>();
            _reportManager = _services.GetService<IReportManager>();

            if (_logger == null)
            {
                throw new InvalidOperationException("ILogProvider service is not configured properly.");
            }

            _logger.Info("✅ Global test run setup completed.");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var scenarioName = _context.ScenarioInfo.Title;
            _logger?.Info($"--- Starting scenario: {scenarioName} ---");
            _logger?.Info($"🔷 Starting scenario: {scenarioName}");
            _reportManager?.CreateTest(scenarioName);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var scenarioName = _context.ScenarioInfo.Title;
            _logger?.Info($"--- Ending scenario: {scenarioName} ---");
            _logger?.Info($"🔶 Ending scenario: {scenarioName}");
        }

        [AfterTestRun]
        public static void GlobalTeardown()
        {
            _logger?.Info("All tests completed. Cleaning up...");
            _logger?.Info("🧹 All tests completed. Performing cleanup...");
            _reportManager?.Flush();
            Serilog.Log.CloseAndFlush();
        }

        /// <summary>
        /// 🔁 In Future: Add AllureReportManager.cs
        /// You’ll just:
        /// 
        /// 1. Create another class `AllureReportManager` implementing `IReportManager`
        /// 2. Update DI in TestHooks.cs:
        /// 
        ///     services.AddSingleton&lt;IReportManager, AllureReportManager&gt;();
        /// 
        /// ✅ No need to touch any test logic or reporting interface.
        /// That’s the power of abstraction and DI.
        /// </summary>


        /// <summary>
        /// 📂 Project Folder Structure Overview:
        ///
        /// YourTestFramework/
        /// ├── Features/                  # BDD feature files (.feature)
        /// ├── StepDefinitions/           # Step bindings for Reqnroll (SpecFlow)
        /// ├── Hooks/                     # Global test hooks (Before/After scenario/run)
        /// │   └── TestHooks.cs
        /// ├── Utilities/                 # Shared utilities like ConfigurationReader
        /// │   └── ConfigurationReader.cs
        /// ├── Helpers/                   # Support services and cross-cutting concerns
        /// │   ├── Logging/               # Logging abstraction
        /// │   │   └── ILogProvider.cs, SerilogProvider.cs
        /// │   ├── Reporting/             # Reporting abstraction (e.g., ExtentReports)
        /// │   │   └── IReportManager.cs, ExtentReportManager.cs
        /// │   └── Environment/           # Multi-environment configuration helpers
        /// │       └── IEnvironmentHelper.cs, EnvironmentHelper.cs
        /// ├── TestData/                  # JSON files for request/response test data
        /// │   └── *.json
        /// └── appsettings.json           # Config file for environment, base URLs, tokens, etc.
        ///
        /// 💡 Tip: This structure is modular and built for future enhancements:
        ///     - Add Allure, Playwright, GraphQL, DB support, etc.
        ///     - DI-ready for plug-and-play services
        /// </summary>


    }
}
