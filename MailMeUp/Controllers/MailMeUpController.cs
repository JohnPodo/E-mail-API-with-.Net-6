using CustomHandlers;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailMeUp.Controllers
{ 
    public class MailMeUpController : SuperController
    {
        
        [HttpPost]
        public async Task<ActionResult<EmailResponse>> SendEmail(MailDto dto)
        {
            EmailHandler handler = new EmailHandler();
            var authCheck = await CheckAuth(false, false);
            if (authCheck is not null) return authCheck;
            await WriteRequestInfoToLog(dto);
            if (dto is null)
                return new BadRequestObjectResult(new EmailResponse() { Success = false, ErrorMessage = "No Data was given" });
            if (dto.To is null)
                return new BadRequestObjectResult(new EmailResponse() { Success = false, ErrorMessage = "No Receiver was provided" });
            if (dto.To.Count == 0)
                return new BadRequestObjectResult(new EmailResponse() { Success = false, ErrorMessage = "No Receiver was provided" });
            var result = handler.SendEmail(dto, _UserHandler._SessionUser);
            return result;
        }
         
    }
}
