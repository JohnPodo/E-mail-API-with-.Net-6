using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repos
{
    public class LogRepo : MasterRepo
    {
         public async Task AddToLog(LogMeUp model)
        {
            await _DbContext.Logs.AddAsync(model);
            await _DbContext.SaveChangesAsync();    
        }

        public async Task DeleteToLog(LogMeUp model)
        {
            var logToDel = await _DbContext.Logs.Where(l => l.Id == model.Id).FirstOrDefaultAsync();
            if (logToDel is null)
                return;
            _DbContext.Logs.Remove(logToDel);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<List<LogMeUp>> GetAllLogs()
        {
            return await _DbContext.Logs.ToListAsync();
        }

        public async Task<List<LogMeUp>> GetAllLogsOfSession(string processSession)
        {
            if (string.IsNullOrEmpty(processSession))
                return null;
            var logs = await _DbContext.Logs.Where(l => l.ProcessSession.ToString() == processSession).ToListAsync();
            return logs;
        }

        public async Task DeleteAllLogsOfSession(string processSession)
        {
            if (string.IsNullOrEmpty(processSession))
                return;
            var logs = await GetAllLogsOfSession(processSession);
            logs.ForEach(async l => await DeleteToLog(l));
        }

        public async Task<List<List<LogMeUp>>> GetSessionsWithException()
        {
            var listOfLogsWithException = await _DbContext.Logs.Where(s=>s.Severity == Severity.Exception).ToListAsync();
            var data = new List<List<LogMeUp>>();
            foreach (var log in listOfLogsWithException)
            {
                data.Add(await GetAllLogsOfSession(log.ProcessSession.ToString()));
            }
            return data;
        }
    }
}
