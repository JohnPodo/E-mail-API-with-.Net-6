using CustomValidations;
using Dtos;
using Models;
using Models.Responses;
using System.Net;
using System.Net.Mail;

namespace CustomHandlers
{
    public class EmailHandler
    {
        private readonly MailDtoValidator validator;
        public EmailHandler()
        {
            validator = new MailDtoValidator();
        }
        public EmailResponse SendEmail(MailDto mail,MailMeUpUser user)
        {
            try
            {
                var firstCheck = validator.ValidateMeMail(mail);
                if (firstCheck is not null)
                    return firstCheck;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = GetFromMailAddress(user);
                mail.To.ForEach(to=>message.To.Add(to));
                message.Subject = mail.Subject;
                message.IsBodyHtml = mail.IsBodyHtml;
                message.Body = mail.Body;
                if(mail.Attachments is not null)
                {
                    var attachments = GetAttachments(mail.Attachments);
                    if (attachments is null)
                        return new EmailResponse() { Success = false, ErrorMessage = "Error on parsing attachments" };
                    attachments.ForEach(a => message.Attachments.Add(a));
                } 
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                var creds = GetCredentials(user);
                if (creds is null)
                    return new EmailResponse() { Success = false, ErrorMessage = "Could not create credentials" };
                smtp.Credentials = creds;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return new EmailResponse() { Success = true, ErrorMessage = string.Empty };
            }
            catch (Exception ex)
            {//ToDo:Log Exception
                return new EmailResponse() { Success = false, ErrorMessage = "Error while sending e-mail" };
            }
        }

        private List<Attachment> GetAttachments(List<MailMeUpAttachmentDto> info)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach (var attachment in info)
            {
                try
                {
                    attachments.Add(Attachment.CreateAttachmentFromString(attachment.Base64Content, attachment.TitleWithExtension));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    return null;
                }

            }
            return attachments;
        }

        private NetworkCredential GetCredentials(MailMeUpUser user)
        { 
            var username = user.EmailUsername;
            var password = user.EmailPassword;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            return new NetworkCredential(username, password);
        }

        private MailAddress GetFromMailAddress(MailMeUpUser user)
        { 
            string mailAddress = user.EmailAddress;
            return new MailAddress(mailAddress);
        }
    }
}