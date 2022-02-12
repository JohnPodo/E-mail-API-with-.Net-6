using CustomHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
