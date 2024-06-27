using Framework.Core.comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Entity
{
    public class Department : ISoftDelete,ICreationTimeSignature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Student> Students { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<Professor> Professors { get; set; } = new();
        public DateTime CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
