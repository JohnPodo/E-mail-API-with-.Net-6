using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class MailDto
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<MailMeUpAttachmentDto> Attachments { get; set; }

        public MailDto()
        {
            To = new List<string>();
            Attachments = new List<MailMeUpAttachmentDto>();
        }
    }
}
