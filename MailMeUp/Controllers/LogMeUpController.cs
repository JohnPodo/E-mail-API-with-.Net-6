using Microsoft.AspNetCore.Mvc; 
using Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailMeUp.Controllers
{ 
    public class LogMeUpController : SuperController
    { 
        [HttpGet]
        public async Task<ActionResult<LogResponse>> GetAllLogs()
        {
            var authCheck = await CheckAuth(true,true);
            if (authCheck is not null) return authCheck;
            var response = await _LogHandler.GetAllLogs();
            await _UserHandler.EraseToken();
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult<LogResponse>> GetSessionsWithException()
        {
            var authCheck = await CheckAuth(true, true);
            if (authCheck is not null) return authCheck;
            var response = await _LogHandler.GetSessionsWithException();
            await _UserHandler.EraseToken();
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse>> DeleteAllLogs()
        {
            var authCheck = await CheckAuth(true, true);
            if (authCheck is not null) return authCheck;
            var response = await _LogHandler.DeleteAllLogs();
            await _UserHandler.EraseToken();
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet("{processSession}")]
        public async Task<ActionResult<LogResponse>> GetAllLogsOfSession(Guid processSession)
        {
            var authCheck = await CheckAuth(true, true);
            if (authCheck is not null) return authCheck;
            var response = await _LogHandler.GetAllLogsOfSession(processSession);
            await _UserHandler.EraseToken();
            await WriteRequestInfoToLog(processSession);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet("{processSession}")]
        public async Task<ActionResult<BaseResponse>> DeleteAllLogsOfSession(Guid processSession)
        {
            var authCheck = await CheckAuth(true, true);
            if (authCheck is not null) return authCheck;
            var response = await _LogHandler.DeleteAllLogsOfSession(processSession);
            await _UserHandler.EraseToken();
            await WriteRequestInfoToLog(processSession);
            await WriteResponseInfoToLog(response);
            return response;
        }
         
    }
}
