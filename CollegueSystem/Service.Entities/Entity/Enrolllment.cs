using Framework.Core.comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Entity
{
    public class Enrolllment 
    {
        public int CrsId { get; set; }
        public int StdId { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
