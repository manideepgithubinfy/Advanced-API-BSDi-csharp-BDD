using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Interfaces
{
    public interface IReportManager
    {
        void CreateTest(string testName);
        void LogInfo(string message);
        void LogError(string message);
        void Flush();
    }

}
