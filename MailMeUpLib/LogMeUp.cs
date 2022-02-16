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
    public class LogMeUp : MasterMeUp
    { 
        public LogMeUp(string url) :base(url)
        { 
        }
        public async Task<BaseResult<LogResponse>> GetAlllogs(string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<LogResponse>(Method.Get, $"/api/LogMeUp/GetAlllogs", token,null);
                var response = await client.ExecuteAsync<LogResponse>(request);
                if (response is null)
                    return new BaseResult<LogResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<LogResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<LogResponse>(false, ex.Message, HttpStatusCode.OK, null);
            } 
        }

        public async Task<BaseResult<LogResponse>> GetSessionsWithException(string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<LogResponse>(Method.Get, $"/api/LogMeUp/GetSessionsWithException", token,null);
                var response = await client.ExecuteAsync<LogResponse>(request);
                if (response is null)
                    return new BaseResult<LogResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<LogResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<LogResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }

        public async Task<BaseResult<BaseResponse>> DeleteAllLogs(string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<BaseResponse>(Method.Get, $"/api/LogMeUp/DeleteAllLogs", token,null);
                var response = await client.ExecuteAsync<BaseResponse>(request);
                if (response is null)
                    return new BaseResult<BaseResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<BaseResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<BaseResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }

        public async Task<BaseResult<LogResponse>> GetAllLogsOfSession(Guid session,string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<LogResponse>(Method.Get, $"/api/LogMeUp/GetAllLogsOfSession/{session}", token,null);
                var response = await client.ExecuteAsync<LogResponse>(request);
                if (response is null)
                    return new BaseResult<LogResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<LogResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<LogResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }
        public async Task<BaseResult<LogResponse>> DeleteAllLogsOfSession(Guid session, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<LogResponse>(Method.Get, $"/api/LogMeUp/DeleteAllLogsOfSession/{session}", token,null);
                var response = await client.ExecuteAsync<LogResponse>(request);
                if (response is null)
                    return new BaseResult<LogResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<LogResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<LogResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        } 
    }
}
