using CustomExceptions;
using System.Text.RegularExpressions;

namespace Dtos
{
    public class MailMeUpAttachmentDto
    {
        public MailMeUpAttachmentDto()
        {

        }
        public MailMeUpAttachmentDto(string content, string titleWithExtension, bool isItBase64)
        {
            var extension = Path.GetExtension(titleWithExtension);
            if (string.IsNullOrWhiteSpace(extension))
                throw new MailMeUpException("Title of attachment does not contain extension");
            if (string.IsNullOrWhiteSpace(content))
                throw new MailMeUpException("Content of attachment cannot be null or empty");
            if (string.IsNullOrWhiteSpace(titleWithExtension))
                throw new MailMeUpException("Title of attachment cannot be null or empty");
            if (isItBase64)
                Base64Content = content;
            else
                Base64Content = DecodeRawContent(content);

            IsBase64(Base64Content);

            TitleWithExtension = titleWithExtension;
        }


        public string Base64Content { get; set; }
        public string TitleWithExtension { get; set; }
        public void IsBase64(string base64String)
        {
            if (!Regex.IsMatch(base64String, @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{4})$") || base64String.Length % 4 != 0)
                throw new MailMeUpException("Not a valid base64 string");

            try
            {
                Convert.FromBase64String(base64String);
            }
            catch
            {
                throw new MailMeUpException("Not a valid base64 string");
            }
        }
        private string DecodeRawContent(string content)
        {
            byte[] data = System.Convert.FromBase64String(content);
            var base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
    }
}