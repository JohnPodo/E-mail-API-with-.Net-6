using System.Net;

namespace MailMeUpLib
{
    public class BaseResult<T> where T : class
    {
        public BaseResult(bool IsSuccesfull,string errorMsg,HttpStatusCode code,T data)
        {
            Success = IsSuccesfull;
            ErrorMessage = errorMsg;
            StatusCode = code;
            Data = data;
        }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
