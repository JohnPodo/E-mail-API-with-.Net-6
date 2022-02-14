using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class MailMeUpUser
    {
        public int Id { get; set; }

        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string EmailUsername { get; set; }
        [JsonIgnore]
        public string EmailPassword { get; set; }
        public string EmailAddress { get; set; }
        public Guid? ActiveToken { get; set; } 
    }
}
