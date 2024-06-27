using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Models.ViewModels
{
    public class CourseVM
    {
        public string Name { get; set; }
        public int NumOfHours { get; set; }
        public int DeptId { get; set; }
        public int ProfId { get; set; }
    }
}
