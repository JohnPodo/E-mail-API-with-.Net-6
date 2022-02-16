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
    public class UserMeUp : MasterMeUp
    { 
        public UserMeUp(string url) :base(url)
        { 
        }

        public async Task<BaseResult<UserResponse>> GetUserById(int id, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<UserResponse>(Method.Get, $"/api/UserMeUp/GetUserById/{id}", token,null);
                var response = await client.ExecuteAsync<UserResponse>(request);
                if (response is null)
                    return new BaseResult<UserResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<UserResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<UserResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }

        public async Task<BaseResult<UserResponse>> DeleteUser(int id, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<UserResponse>(Method.Get, $"/api/UserMeUp/DeleteUser/{id}", token,null);
                var response = await client.ExecuteAsync<UserResponse>(request);
                if (response is null)
                    return new BaseResult<UserResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<UserResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<UserResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }

        public async Task<BaseResult<UserResponse>> GetUserById(Guid session, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest<UserResponse>(Method.Get, $"/api/UserMeUp/GetUserByActiveSession/{session}", token, null);
                var response = await client.ExecuteAsync<UserResponse>(request);
                if (response is null)
                    return new BaseResult<UserResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<UserResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<UserResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }

        public async Task<BaseResult<BaseResponse>> RegisterUser(UserDto dto)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest(Method.Post, $"/api/UserMeUp/RegisterUser", null,dto);
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
        
        public async Task<BaseResult<BaseResponse>> RegisterAdmin(UserDto dto,string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest(Method.Post, $"/api/UserMeUp/RegisterAdmin", token, dto);
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

        public async Task<BaseResult<BaseResponse>> ChangePassword(ChangePasswordDto dto, string token)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest(Method.Post, $"/api/UserMeUp/ChangePassword", token, dto);
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

        public async Task<BaseResult<LoginResponse>> Login(LoginDto dto)
        {
            try
            {
                var client = new RestClient(_Url);
                var request = ConstructRequest(Method.Post, $"/api/UserMeUp/Login", null, dto);
                var response = await client.ExecuteAsync<LoginResponse>(request);
                if (response is null)
                    return new BaseResult<LoginResponse>(false, "Response came back null", HttpStatusCode.NoContent, null);
                var result = new BaseResult<LoginResponse>(response.IsSuccessful, response.ErrorMessage, response.StatusCode, response.Data);
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResult<LoginResponse>(false, ex.Message, HttpStatusCode.OK, null);
            }
        }
    }
}
