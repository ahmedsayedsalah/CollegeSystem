using AutoMapper;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Course;
using Service.Core.Models.ViewModels.Department;
using Service.Core.Models.ViewModels.Professor;
using Service.Core.Models.ViewModels.Student;
using Service.Core.Models.ViewModels.User;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department,DepartmentVM>().ReverseMap();
            CreateMap<Department,UpdateDepartmentVM>().ReverseMap();
            CreateMap<Professor,ProfessorVM>().ReverseMap();
            CreateMap<Professor,UpdateProfessorVM>().ReverseMap();
            CreateMap<Student,StudentVM>().ReverseMap();
            CreateMap<Student,UpdateStudentVM>().ReverseMap();
            CreateMap<Course,CourseVM>().ReverseMap();
            CreateMap<Course,UpdateCourseVM>().ReverseMap();
            CreateMap<User,UserVM>().ReverseMap();
            CreateMap<User, UpdateUserVM>().ReverseMap();
            CreateMap<User,RegisterUserVM>().ReverseMap();
        }
    }
}
