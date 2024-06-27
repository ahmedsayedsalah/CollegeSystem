using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Department;

namespace Service.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<IList<DepartmentVM>> GetAll()
        {
            var departments= await departmentService.GetAll();

            return departments;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<DepartmentVM> GetById(int id)
        {
            var department= await departmentService.GetById(id);

            return department;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task<int> Add(DepartmentVM department)
        {
            var deptId= await departmentService.Add(department);

            return deptId;
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task Update(UpdateDepartmentVM department)
        {
            await departmentService.Update(department);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task Delete(int id)
        {
            await departmentService.Delete(id);
        }

        [HttpPost]
        [Route("AddProfessors/{deptId}")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task AddProfessors(int deptId, List<ProfessorVM> professors)
        {
            await departmentService.AddProfessors(deptId, professors);
        }

        [HttpPost]
        [Route("AddCourses/{deptId}")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task AddCourses(int deptId, List<CourseVM> courses)
        {
            await departmentService.AddCourses(deptId, courses);
        }

        [HttpPost]
        [Route("AddStudents/{deptId}")]
        [Authorize(Policy = "DepartmentManagePolicy")]
        public async Task AddStudents(int deptId, List<StudentVM> students)
        {
            await departmentService.AddStudents(deptId, students);
        }

        [HttpGet]
        [Route("GetProfessors/{deptId}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<IList<ProfessorVM>> GetProfessors(int deptId)
        {
            var query = await departmentService.GetProfessors(deptId);
            return query;
        }

        [HttpGet]
        [Route("GetCourses/{deptId}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<IList<CourseVM>> GetCourses(int deptId)
        {
            var query = await departmentService.GetCourses(deptId);
            return query;
        }

        [HttpGet]
        [Route("GetStudents/{deptId}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<IList<StudentVM>> GetStudents(int deptId)
        {
            var query = await departmentService.GetStudents(deptId);
            return query;
        }

        [HttpGet]
        [Route("GetCoursesNames/{deptId}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<IList<string>> GetCoursesNames(int deptId)
        {
            var names= await departmentService.GetNamesOfCourses(deptId);

            return names;
        }

        [HttpGet]
        [Route("GetNumberOfStudents/{deptId}")]
        [Authorize(Policy = "DepartmentViewPolicy")]
        public async Task<long> GetNumberOfStudents(int deptId)
        {
            var count= await departmentService.GetNumberOfStudents(deptId);

            return count;
        }

    }
}
