using Dtos;
using Models.Responses;

namespace CustomValidations
{
    public class MailDtoValidator
    {
        public EmailResponse ValidateMeMail(MailDto mail)
        {
            if(mail == null)
                return new EmailResponse() { Success = false,ErrorMessage="No Data given"};
            if (mail.To == null)
                return new EmailResponse() { Success = false, ErrorMessage = "Don't know where to send e-mails" };
            return null;
        }
    }
}