using Microsoft.AspNetCore.Mvc;
using Service.Business.Services;
using Service.Core.Models.ViewModels.Professor;
using Service.Core.Models.ViewModels;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Student;
using Service.Entities.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "StudentViewPolicy")]
        public async Task<IList<StudentVM>> GetAll()
        {
            var students = await studentService.GetAll();

            return students;
        }

        [HttpGet]
        [Route("paginate/{pageIndex}/{pageSize}")]
        [Authorize(Policy = "StudentViewPolicy")]
        public async Task<IList<StudentVM>> Paginate(int pageIndex, int pageSize)
        {
            var students = await studentService.Paginate(pageIndex, pageSize);
            return students;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Policy = "StudentViewPolicy")]
        public async Task<StudentVM> GetById(int id)
        {
            var student = await studentService.GetById(id);
            return student;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "StudentManagePolicy")]
        public async Task<int> Add(StudentVM professor)
        {
            var stdId = await studentService.Add(professor);
            return stdId;
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = "StudentManagePolicy")]
        public async Task Update(UpdateStudentVM student)
        {
            await studentService.Update(student);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Policy = "StudentManagePolicy")]
        public async Task Delete(int id)
        {
            await studentService.Delete(id);
        }

        [HttpPost]
        [Route("EnrollCourse/{stdId}/{crsId}")]
        [Authorize(Policy = "StudentManagePolicy")]
        public async Task EnrollCourse(int stdId,int crsId)
        {
            await studentService.EnrollCourse(stdId, crsId);
        }

        [HttpGet]
        [Route("GetEnrolledCourses/{stdId}")]
        [Authorize(Policy = "StudentViewPolicy")]
        public async Task<IList<string>> GetEnrolledCourses(int stdId)
        {
            return await studentService.GetEnrolledCourses(stdId);
        }

        [HttpGet]
        [Route("GetNumberOfEnrolledCourses/{stdId}")]
        [Authorize(Policy = "StudentViewPolicy")]
        public async Task<long> GetNumberOfEnrolledCourses(int stdId)
        {
            return await studentService.GetNumberOfEnrolledCourses(stdId);
        }
    }
}
