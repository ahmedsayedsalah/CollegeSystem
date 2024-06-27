using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.comman
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public async Task DeleteAsync()
        {
            IsDeleted = true;
            DeleteTime = DateTime.Now;
            await Task.CompletedTask;
        }
        public async Task UnDeleteAsync()
        {
            IsDeleted = false;
            DeleteTime = null;
            await Task.CompletedTask;
        }
    }
}
