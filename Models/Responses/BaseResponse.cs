using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
