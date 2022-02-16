using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailMeUpLib
{
    public abstract class MasterMeUp
    {
        protected readonly string _Url;
        protected MasterMeUp(string url)
        {
            _Url = url;
        }
        protected RestRequest ConstructRequest<T>(Method method, string endpoint, string token, T body) where T : class
        {
            var request = new RestRequest(endpoint);
            request.Method = method;
            request.AddHeader("User-Agent", Environment.MachineName);
            if (!string.IsNullOrEmpty(token))
                request.AddHeader("token", token);
            request.RequestFormat = DataFormat.Json;
            if (body != null)
                request.AddJsonBody(body);
            return request;
        }
    }
}
