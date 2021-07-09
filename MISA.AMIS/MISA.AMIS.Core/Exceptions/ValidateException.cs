using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Exceptions
{
    public class ValidateExceptions:Exception
    {
        public string UserMessenger = string.Empty;
        public ValidateExceptions(string msg, object data = null) : base(msg, new Exception())
        {
            this.UserMessenger = msg;
        }
    }
}
