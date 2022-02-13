using CustomHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MailMeUp.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public abstract class SuperController : ControllerBase
    {
        protected readonly LogHandler _LogHandler;

        public SuperController()
        {
            _LogHandler = new LogHandler();
        }

        protected async Task WriteRequestInfoToLog<T>(T model)
        {
            await _LogHandler.WriteToLog("Received Request", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Path --> {JsonConvert.SerializeObject(this.Request.Path, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Headers --> {JsonConvert.SerializeObject(this.Request.Headers, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Body --> {JsonConvert.SerializeObject(model, Formatting.Indented)}", Models.Severity.Information);
        }

        protected async Task WriteResponseInfoToLog<T>(T response)
        {
            await _LogHandler.WriteToLog($"Response --> {JsonConvert.SerializeObject(response, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog("Request was answered in success", Models.Severity.Information);
        }
    }
}
