using Framework.Core.comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Entity
{
    public class Student : ISoftDelete, ICreationTimeSignature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
        //public List<Course> Courses { get; set; } = new();
        public List<Enrolllment> Enrolllments { get; set; } = new();
        public DateTime CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
