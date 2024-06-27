using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public class UnAuthorizedException : BaseException
    {
        public UnAuthorizedException():base("UnAuthorizedException") { }
        public UnAuthorizedException(string message):base(message) { }
        
    }
}
