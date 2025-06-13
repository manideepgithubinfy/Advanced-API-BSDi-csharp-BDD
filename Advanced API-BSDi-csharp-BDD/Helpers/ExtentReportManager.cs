using Advanced_API_BSDi_csharp_BDD.Interfaces;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Helpers
{
    public class ExtentReportManager : IReportManager
    {
        private readonly ExtentReports _extent;
        private ExtentTest? _test;
        private readonly ExtentSparkReporter _reporter;

        public ExtentReportManager()
        {
            _extent = new ExtentReports(); // Ensure _extent is initialized
            _reporter = new ExtentSparkReporter("Reports/ExtentReport.html");
            _reporter.Config.Theme = Theme.Standard;
            _extent.AttachReporter(_reporter);
        }

        public void CreateTest(string testName)
        {
            if (_extent == null) throw new InvalidOperationException("ExtentReports instance is not initialized.");
            _test = _extent.CreateTest(testName);
        }

        public void LogInfo(string message)
        {
            _test?.Info(message);
        }

        public void LogError(string message)
        {
            _test?.Fail(message);
        }

        public void Flush()
        {
            _extent?.Flush();
        }
    }
}
