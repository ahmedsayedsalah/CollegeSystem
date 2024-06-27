using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public class ItemAlreadyExistException : BaseException
    {
        public ItemAlreadyExistException():base("ItemAlreadyExistException") { }
        public ItemAlreadyExistException(string Message) : base(Message) { }
    }
}
