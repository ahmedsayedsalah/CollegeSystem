using Microsoft.AspNetCore.Mvc;
using Service.Business.Services;
using Service.Core.Models.ViewModels.Student;
using Service.Core.Models.ViewModels;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CourseController : ControllerBase
    {
        private ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "CourseViewPolicy")]
        public async Task<IList<CourseVM>> GetAll()
        {
            var courses = await courseService.GetAll();

            return courses;
        }

        [HttpGet]
        [Route("paginate/{pageIndex}/{pageSize}")]
        [Authorize(Policy = "CourseViewPolicy")]
        public async Task<IList<CourseVM>> Paginate(int pageIndex, int pageSize)
        {
            var courses = await courseService.Paginate(pageIndex, pageSize);
            return courses;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Policy = "CourseViewPolicy")]
        public async Task<CourseVM> GetById(int id)
        {
            var course = await courseService.GetById(id);
            return course;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "CourseManagePolicy")]
        public async Task<int> Add(CourseVM professor)
        {
            var crsId = await courseService.Add(professor);
            return crsId;
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = "CourseManagePolicy")]
        public async Task Update(UpdateCourseVM student)
        {
            await courseService.Update(student);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Policy = "CourseManagePolicy")]
        public async Task Delete(int id)
        {
            await courseService.Delete(id);
        }

        [HttpPost]
        [Route("AddStudents/{crsId}")]
        [Authorize(Policy = "CourseManagePolicy")]
        public async Task AddStudents(int crsId,IList<int> students)
        {
            await courseService.AddStudents(crsId, students);
        }

        [HttpGet]
        [Authorize]
        [Route("GetStudents/{crsId}")]
        [Authorize(Policy = "CourseViewPolicy")]
        public async Task<IList<StudentVM>> GetStudents(int crsId)
        {
           return  await courseService.GetStudents(crsId);
        }
    }
}
