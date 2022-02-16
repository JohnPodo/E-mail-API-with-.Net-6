using Dtos;
using Models.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MailMeUpLib
{
    public class MailMeUp
    {
        private readonly string _Url;
        public readonly UserMeUp  User;
        public readonly LogMeUp  Log;
        public MailMeUp(string url)
        {
            _Url = url;
            User = new UserMeUp(url);
            Log = new LogMeUp(url);
        }

        public async Task<BaseResult<EmailResponse>> SendEmail(MailDto dto, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = new RestRequest("/api/MailMeUp/SendEmail");
                request.Method = Method.Post;
                request.AddHeader("User-Agent", Environment.MachineName);
                request.AddHeader("token", token);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(dto);
                var response = await client.ExecuteAsync<EmailResponse>(request);
                if (response is null)
                    return new BaseResult<EmailResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<EmailResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<EmailResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }
    }
}
