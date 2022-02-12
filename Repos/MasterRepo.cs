using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class MasterRepo
    {
        protected readonly MailMeUpContext _DbContext;
        public MasterRepo()
        {
            _DbContext = new MailMeUpContext();
        }
    }
}
