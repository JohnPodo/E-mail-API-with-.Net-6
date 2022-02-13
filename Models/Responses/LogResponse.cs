using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class LogResponse : BaseResponse
    {
        public List<LogMeUp> Logs { get; set; }
    }
}
