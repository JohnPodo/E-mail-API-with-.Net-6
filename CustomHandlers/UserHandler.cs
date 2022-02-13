using Dtos;
using Models;
using Models.Responses;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomHandlers
{
    public class UserHandler
    {
        private readonly UserRepo _Repo;
        public MailMeUpUser? _SessionUser;
        private readonly LogHandler _LogHandler;

        public UserHandler(LogHandler logrepo)
        {
            _Repo = new UserRepo();
            _LogHandler = logrepo;
        }

        public async Task<bool> GetSessionUser(Guid sessionToken, bool mustBeAdmin)
        {
            var user = await _Repo.GetUserByActiveToken(sessionToken);
            if (user is null) return false;
            _SessionUser = user;
            return user.IsAdmin == mustBeAdmin;
        }

        public async Task<UserResponse> GetUserByActiveToken(Guid sessionToken)
        {
            try
            {
                var user = await _Repo.GetUserByActiveToken(sessionToken);
                if (user is null) return new UserResponse() { Success = false, ErrorMessage = "No User Found" };
                await EraseToken();
                return new UserResponse() { Success = true, User = user };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in GetUserByActiveToken with message {ex.Message}", Severity.Exception);
                return new UserResponse() { Success = false, ErrorMessage = "Error on process" };
            }

        }

        public async Task<UserResponse> GetUserById(int id)
        {
            try
            {
                var user = await _Repo.GetUserById(id);
                if (user is null) return new UserResponse() { Success = false, ErrorMessage = "No User Found" };
                await EraseToken();
                return new UserResponse() { Success = true, User = user };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in GetUserById with message {ex.Message}", Severity.Exception);
                return new UserResponse() { Success = false, ErrorMessage = "Error on process" };
            }
        }

        public async Task<BaseResponse> RegisterUser(UserDto dto)
        {
            try
            {
                MailMeUpUser userToAdd = new MailMeUpUser()
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    EmailAddress = dto.EmailAddress,
                    EmailPassword = dto.EmailPassword,
                    EmailUsername = dto.EmailUsername,
                    IsAdmin = false
                };
                await _Repo.AddUser(userToAdd);
                return new BaseResponse() { Success = true, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in RegisterUser with message {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on process" };
            }
           
        }

        public async Task<BaseResponse> RegisterAdmin(UserDto dto)
        {
            try
            {
                MailMeUpUser userToAdd = new MailMeUpUser()
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    EmailAddress = dto.EmailAddress,
                    EmailPassword = dto.EmailPassword,
                    EmailUsername = dto.EmailUsername,
                    IsAdmin = true
                };
                await _Repo.AddUser(userToAdd);
                await EraseToken();
                return new BaseResponse() { Success = true, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in RegisterUser with message {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on process" };
            }

        }

        public async Task<BaseResponse> DeleteUser(int id)
        {
            try
            {
                 
                await _Repo.DeleteUser(id);
                await EraseToken();
                return new BaseResponse() { Success = true, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in RegisterUser with message {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on process" };
            }

        }
        
        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return new LoginResponse() { Success = false,ErrorMessage="No Credentials given" };
            var userInDb = await _Repo.GetUserFromUsername(loginDto.Username);
            if(userInDb is null) return new LoginResponse() { Success = false, ErrorMessage = "Invalid Credentials" };
            if(userInDb.ActiveToken is not null) return new LoginResponse() { Success = false, ErrorMessage = "Already Logged in" };
                if (userInDb.Password != loginDto.Password) return new LoginResponse() { Success = false, ErrorMessage = "Invalid Credentials" };
            userInDb.ActiveToken = Guid.NewGuid();
            await _Repo.UpdateUser(userInDb);
            return new LoginResponse() { Success = true, Token = userInDb.ActiveToken.ToString() };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in Login with message {ex.Message}", Severity.Exception);
                return new LoginResponse() { Success = false, ErrorMessage = "Error on process" };
            }
        }

        public async Task EraseToken()
        {
            if(_SessionUser.ActiveToken is not null){
                _SessionUser.ActiveToken = null;
                await _Repo.UpdateUser(_SessionUser);
            }
        }

        public async Task<BaseResponse> ChangePassword(ChangePasswordDto dto)
        {
            try
            {
                if (_SessionUser.Password != dto.OldPassword)
                    return new BaseResponse() { Success = false, ErrorMessage = "Invalid Creds" };

                _SessionUser.Password = dto.NewPassword;
                _SessionUser.ActiveToken = null;
                await _Repo.UpdateUser(_SessionUser);
                return new BaseResponse() { Success = true };
            }
            catch (Exception ex)
            {
                await _LogHandler.WriteToLog($"Exception Caught in ChangePassword with message {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on process" };
            }
        }
    }
}
