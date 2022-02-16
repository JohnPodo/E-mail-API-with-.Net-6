using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MailMeUpLib
{
    public class BaseResult<T> where T : class
    {
        public BaseResult(bool IsSuccesfull,string errorMsg,HttpStatusCode code,T Data)
        {
            Success = IsSuccesfull;
            ErrorMessage = errorMsg;
            StatusCode = code;
            Data = Data;
        }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
