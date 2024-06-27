using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() : base("ForbiddenException") { }
        public ForbiddenException(string message) : base(message) { }
    }
}
