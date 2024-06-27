using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public interface ICreationTimeSignature
    {
        public DateTime CreateTime { get; set; }

        public async Task Create()
        {
            CreateTime = DateTime.Now;
            await Task.CompletedTask;
        }
    }
}
