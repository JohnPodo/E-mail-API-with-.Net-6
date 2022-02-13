using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repos
{
    public class LogRepo : MasterRepo
    {
         public async Task AddToLog(LogMeUp model)
        {
            await _DbContext.MailMeUpUserLogs.AddAsync(model);
            await _DbContext.SaveChangesAsync();    
        }

        public async Task DeleteLog(LogMeUp model)
        {
            var logToDel = await _DbContext.MailMeUpUserLogs.Where(l => l.Id == model.Id).FirstOrDefaultAsync();
            if (logToDel is null)
                return;
            _DbContext.MailMeUpUserLogs.Remove(logToDel);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<List<LogMeUp>> GetAllLogs()
        {
            return await _DbContext.MailMeUpUserLogs.ToListAsync();
        }

        public async Task<List<LogMeUp>> GetAllLogsOfSession(Guid processSession)
        {
            if (processSession == Guid.Empty)
                return null;
            var logs = await _DbContext.MailMeUpUserLogs.Where(l => l.ProcessSession == processSession).ToListAsync();
            return logs;
        }

        public async Task DeleteAllLogsOfSession(Guid processSession)
        {
            if (processSession == Guid.Empty)
                return;
            var logs = await GetAllLogsOfSession(processSession);
            logs.ForEach(async l => await DeleteLog(l));
        }

        public async Task<List<LogMeUp>> GetSessionsWithException()
        {
            var listOfLogsWithException = await _DbContext.MailMeUpUserLogs.Where(s=>s.Severity == Severity.Exception).ToListAsync();
            return listOfLogsWithException;
        }

        public async Task DeleteAllLogs()
        {
            _DbContext.MailMeUpUserLogs.RemoveRange(_DbContext.MailMeUpUserLogs); 
            await _DbContext.SaveChangesAsync();
        }
    }
}
