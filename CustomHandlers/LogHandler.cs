using Models;
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
    }
}
