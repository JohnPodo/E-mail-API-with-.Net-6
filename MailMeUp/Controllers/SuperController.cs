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
        protected readonly UserHandler _UserHandler;
        protected Guid? token;
        public SuperController()
        {
            _LogHandler = new LogHandler();
            _UserHandler = new UserHandler(_LogHandler);

        }

        protected async Task<UnauthorizedResult> CheckAuth(bool mustBeAdmin)
        {
            if (this.Request.Headers.ContainsKey("token"))
                token = new Guid(this.Request.Headers.Where(s => s.Key == "token").FirstOrDefault().Value);
            else token = null;
            if (token is null) return Unauthorized();
            if (!token.HasValue) return Unauthorized();
            if (!await _UserHandler.GetSessionUser(token.Value, mustBeAdmin)) return Unauthorized();
            await _LogHandler.AddUserToLog(_UserHandler._SessionUser);
            return null;
        }

        protected async Task WriteRequestInfoToLog<T>(T model)
        {
            await _LogHandler.WriteToLog("Received Request", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Path --> {JsonConvert.SerializeObject(this.Request.Path, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Headers --> {JsonConvert.SerializeObject(this.Request.Headers, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Request Body --> {JsonConvert.SerializeObject(model, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog($"Session User --> {JsonConvert.SerializeObject(_UserHandler._SessionUser, Formatting.Indented)}", Models.Severity.Information);
        }

        protected async Task WriteResponseInfoToLog<T>(T response)
        {
            await _LogHandler.WriteToLog($"Response --> {JsonConvert.SerializeObject(response, Formatting.Indented)}", Models.Severity.Information);
            await _LogHandler.WriteToLog("Request was answered in success", Models.Severity.Information);
        }
    }
}
