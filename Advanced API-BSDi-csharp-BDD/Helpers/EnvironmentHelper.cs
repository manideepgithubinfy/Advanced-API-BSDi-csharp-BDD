using Advanced_API_BSDi_csharp_BDD.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Helpers
{
    public class EnvironmentHelper : IEnvironmentHelper
    {
        public EnvironmentHelper(IConfiguration config) => _config = config;

        private readonly IConfiguration _config;

        public string GetApiUrl(string key)
        {
            var env = _config["Environment"];
            if (env == null)
            {
                throw new InvalidOperationException("Environment configuration is missing.");
            }

            var apiUrl = _config[$"Endpoints:{env}:{key}"];
            if (apiUrl == null)
            {
                throw new KeyNotFoundException($"API URL for key '{key}' in environment '{env}' is not found.");
            }

            return apiUrl;
        }
    }
}
