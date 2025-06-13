using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Helpers
{
    public class MenuBuilder
    {
        public static string LoadFromJson(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName + "_request.json");
            return File.ReadAllText(path);
        }
    }
}
