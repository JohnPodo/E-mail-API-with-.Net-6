﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class LogMeUp
    {
        public int Id { get; set; }

        public Guid ProcessSession { get; set; }

        public Severity Severity { get; set; }

        public string Message { get; set; }

        public DateTime InsertDate { get; set; }   
    }
}
