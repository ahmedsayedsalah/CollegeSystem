using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public class InvalidOperationException : BaseException
    {
        public InvalidOperationException():base("InvalidOperationException") { }
        public InvalidOperationException(string message) : base(message) { }
    }
}
