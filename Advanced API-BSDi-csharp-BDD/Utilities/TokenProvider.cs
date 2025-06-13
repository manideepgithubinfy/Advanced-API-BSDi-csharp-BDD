using Advanced_API_BSDi_csharp_BDD.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_API_BSDi_csharp_BDD.Utilities
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;
        private string? _cachedToken; // Marked as nullable to address CS8618
        private DateTime _expiry;

        public TokenProvider(IConfiguration config) => _config = config; // Converted to primary constructor to address IDE0290

        public async Task<string> GetBearerTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _expiry)
                return _cachedToken;

            var tokenUrl = _config["Auth:TokenUrl"];
            if (string.IsNullOrEmpty(tokenUrl))
                throw new InvalidOperationException("Token URL is not configured.");

            var clientId = _config["Auth:ClientId"];
            if (string.IsNullOrEmpty(clientId))
                throw new InvalidOperationException("Client ID is not configured.");

            var clientSecret = _config["Auth:ClientSecret"];
            if (string.IsNullOrEmpty(clientSecret))
                throw new InvalidOperationException("Client Secret is not configured.");

            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            {"grant_type", "client_credentials"},
                            {"client_id", clientId},
                            {"client_secret", clientSecret}
                        });

            var response = await client.PostAsync(tokenUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseContent);

            if (json["access_token"] == null || json["expires_in"] == null)
                throw new InvalidOperationException("Invalid token response from server.");

            _cachedToken = json["access_token"]!.ToString();
            _expiry = DateTime.UtcNow.AddSeconds(int.Parse(json["expires_in"]!.ToString()) - 60);

            return _cachedToken;
        }
    }
}
