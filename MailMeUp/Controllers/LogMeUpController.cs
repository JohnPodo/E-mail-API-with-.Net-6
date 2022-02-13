using Microsoft.AspNetCore.Mvc; 
using Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailMeUp.Controllers
{ 
    public class LogMeUpController : SuperController
    { 
        [HttpGet]
        public async Task<LogResponse> GetAllLogs()
        {
            var response = await _LogHandler.GetAllLogs(); 
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet]
        public async Task<LogResponse> GetSessionsWithException()
        {
            var response = await _LogHandler.GetSessionsWithException();
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet]
        public async Task<BaseResponse> DeleteAllLogs()
        {
            var response = await _LogHandler.DeleteAllLogs();
            await WriteRequestInfoToLog<object>(null);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet("{processSession}")]
        public async Task<LogResponse> GetAllLogsOfSession(Guid processSession)
        {
            var response = await _LogHandler.GetAllLogsOfSession(processSession);
            await WriteRequestInfoToLog(processSession);
            await WriteResponseInfoToLog(response);
            return response;
        }

        [HttpGet("{processSession}")]
        public async Task<BaseResponse> DeleteAllLogsOfSession(Guid processSession)
        {
            var response = await _LogHandler.DeleteAllLogsOfSession(processSession);
            await WriteRequestInfoToLog(processSession);
            await WriteResponseInfoToLog(response);
            return response;
        }
         
    }
}
