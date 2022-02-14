using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Responses;
using Repos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailMeUp.Controllers
{ 
    public class UserMeUpController : SuperController
    { 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var authCheck = await CheckAuth(true,true);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(id);
            var result = await _UserHandler.GetUserById(id);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpGet("{session}")]
        public async Task<ActionResult<UserResponse>> GetUserByActiveSession(Guid session)
        {
            var authCheck = await CheckAuth(true,true);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(session);
            var result = await _UserHandler.GetUserByActiveToken(session);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteUser(int id)
        {
            var authCheck = await CheckAuth(true,true);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(id);
            var result = await _UserHandler.DeleteUser(id);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> RegisterUser(UserDto dto)
        {  
            await WriteRequestInfoToLog(dto);
            var result = await _UserHandler.RegisterUser(dto);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> RegisterAdmin(UserDto dto)
        {
            var authCheck = await CheckAuth(true,true);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(dto);
            var result = await _UserHandler.RegisterAdmin(dto);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginDto dto)
        {  
            await WriteRequestInfoToLog(dto);
            var result = await _UserHandler.Login(dto);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> ChangePassword(ChangePasswordDto dto)
        {
            var authCheck = await CheckAuth(false, true);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(dto);
            var result = await _UserHandler.ChangePassword(dto);
            await WriteResponseInfoToLog(result);
            return Ok(result);
        }
    }
}
