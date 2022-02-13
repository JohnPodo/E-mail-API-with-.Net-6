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
    public class LogHandler
    {
        private readonly Guid _ProcessSession;
        private readonly LogRepo _Repo;
        public LogHandler()
        {
           _ProcessSession = Guid.NewGuid();
            _Repo = new LogRepo();
        }

        public async Task WriteToLog(string message,Severity severity)
        {
            if (string.IsNullOrEmpty(message))
                return;
            LogMeUp model = new LogMeUp();
            model.Message = message;
            model.Severity = severity;
            model.ProcessSession = _ProcessSession;
            model.InsertDate = DateTime.Now;
            await _Repo.AddToLog(model);
        }

        public async Task<LogResponse> GetAllLogs()
        {
            try
            {
                var logs = await _Repo.GetAllLogs();
                return new LogResponse() { Success = true, ErrorMessage = String.Empty, Logs = logs };

            }
            catch (Exception ex)
            {
                await WriteToLog($"Exception caught in GetAllLogs of handler with message \n Message -> {ex.Message}", Severity.Exception);
                return new LogResponse() { Success = false, ErrorMessage = "Error on handling request",Logs = null };
            }
        }

        public async Task<LogResponse> GetSessionsWithException()
        {
            try
            {
                var logs = await _Repo.GetSessionsWithException();
                return new LogResponse() { Success = true, ErrorMessage = String.Empty, Logs = logs };

            }
            catch (Exception ex)
            {
                await WriteToLog($"Exception caught in GetAllLogs of handler with message \n Message -> {ex.Message}", Severity.Exception);
                return new LogResponse() { Success = false, ErrorMessage = "Error on handling request", Logs = null };
            }
        }

        public async Task<LogResponse> GetAllLogsOfSession(Guid processSession)
        {
            try
            {
                var logs = await _Repo.GetAllLogsOfSession(processSession);
                return new LogResponse() { Success = true, ErrorMessage = String.Empty, Logs = logs };

            }
            catch (Exception ex)
            {
                await WriteToLog($"Exception caught in GetAllLogsOfSession of handler with message \n Message -> {ex.Message}", Severity.Exception);
                return new LogResponse() { Success = false, ErrorMessage = "Error on handling request", Logs = null };
            }
        }

        public async Task<BaseResponse> DeleteAllLogsOfSession(Guid processSession)
        {
            try
            {
                await _Repo.DeleteAllLogsOfSession(processSession);
                return new BaseResponse() { Success = true, ErrorMessage = String.Empty };

            }
            catch (Exception ex)
            {
                await WriteToLog($"Exception caught in GetAllLogsOfSession of handler with message \n Message -> {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on handling request" };
            }
        }

        public async Task<BaseResponse> DeleteAllLogs()
        {
            try
            {
                await _Repo.DeleteAllLogs();
                return new BaseResponse() { Success = true, ErrorMessage = String.Empty };

            }
            catch (Exception ex)
            {
                await WriteToLog($"Exception caught in GetAllLogsOfSession of handler with message \n Message -> {ex.Message}", Severity.Exception);
                return new BaseResponse() { Success = false, ErrorMessage = "Error on handling request" };
            }
        }
    }
}
