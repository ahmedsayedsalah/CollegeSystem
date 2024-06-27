using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Services;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Department;
using Service.Core.Models.ViewModels.Professor;
using Service.Entities.Entity;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfessorController : ControllerBase
    {
        private IProfessorService professorService;

        public ProfessorController(IProfessorService professorService)
        {
            this.professorService = professorService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Policy = "ProfessorViewPolicy")]
        public async Task<IList<ProfessorVM>> GetAll()
        {
            var professors = await professorService.GetAll();

            return professors;
        }

        [HttpGet]
        [Route("paginate/{pageIndex}/{pageSize}")]
        [Authorize(Policy = "ProfessorViewPolicy")]
        public async Task<IList<ProfessorVM>> Paginate(int pageIndex, int pageSize)
        {
            var professors= await professorService.Paginate(pageIndex, pageSize);

            return professors;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Policy = "ProfessorViewPolicy")]
        public async Task<ProfessorVM> GetById(int id)
        {
            var professor = await professorService.GetById(id);

            return professor;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "ProfessorManagePolicy")]
        public async Task<int> Add(ProfessorVM professor)
        {
            var profId = await professorService.Add(professor);

            return profId;
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = "ProfessorManagePolicy")]
        public async Task Update(UpdateProfessorVM professor)
        {
            await professorService.Update(professor);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Policy = "ProfessorManagePolicy")]
        public async Task Delete(int id)
        {
            await professorService.Delete(id);
        }

        [HttpPost]
        [Route("AddCourses/{profId}")]
        [Authorize(Policy = "ProfessorManagePolicy")]
        public async Task AddCourses(int profId,IList<CourseVM> courses)
        {
            await professorService.AddCourses(profId, courses);
        }

        [HttpGet]
        [Route("GetCourses/{profId}")]
        [Authorize(Policy = "ProfessorViewPolicy")]
        public async Task<IList<CourseVM>> GetCourses(int profId)
        {
            var courses = await professorService.GetCourses(profId);

            return courses;
        }

        [HttpGet]
        [Route("GetNumberOfCourses/{profId}")]
        [Authorize(Policy = "ProfessorViewPolicy")]
        public async Task<long> GetNumberOfCourses(int profId)
        {
            var count = await professorService.GetNumberOfCourses(profId);

            return count;
        }
    }
}
