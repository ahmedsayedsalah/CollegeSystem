using Framework.Core.comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Entity
{
    public class Course : ISoftDelete,ICreationTimeSignature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfHours { get; set; }
        public int DeptId { get; set; }
        public int ProfId { get; set; }
        public Professor Professor { get; set; }
        public Department Department { get; set; }
        //public List<Student> Students { get; set; } = new();
        public List<Enrolllment> Enrolllments { get; set; } = new();
        public bool IsDeleted { get ; set ; }
        public DateTime? DeleteTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
