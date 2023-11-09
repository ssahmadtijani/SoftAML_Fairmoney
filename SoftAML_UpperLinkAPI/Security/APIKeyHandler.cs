using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SoftAML_UpperLinkAPI.Security
{
    public class APIKeyHandler : DelegatingHandler
    {
        private Contexts.Entities _db = new Contexts.Entities();
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            bool isValidAPIKey = false;
            IEnumerable<string> lsHeaders;
            var checkApiKeyExists = request.Headers.TryGetValues("API_KEY", out lsHeaders);
            if (checkApiKeyExists)
            {
                string apiKeyClient = lsHeaders.FirstOrDefault();
                try
                {
                    isValidAPIKey = _db.tblAPIKeys.Count(s => s.status.Value == true && s.apiKey.Equals(apiKeyClient)) > 0;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                
            }
            if (!isValidAPIKey)
                return request.CreateResponse(HttpStatusCode.Forbidden, "Wrong API Key");
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}