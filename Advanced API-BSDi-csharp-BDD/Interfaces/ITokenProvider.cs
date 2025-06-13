using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetBearerTokenAsync();
    }
}
