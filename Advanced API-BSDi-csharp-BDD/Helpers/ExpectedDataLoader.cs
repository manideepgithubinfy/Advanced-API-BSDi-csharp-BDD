using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Helpers
{
    public static class ExpectedDataLoader
    {
        public static string LoadExpectedJson(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName + "_expected.json");
            return File.ReadAllText(path);
        }
    }
}
