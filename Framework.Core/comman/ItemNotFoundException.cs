using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException():base("ItemNotFoundException") { }
        public ItemNotFoundException(string message) : base(message) { }
    }
}
