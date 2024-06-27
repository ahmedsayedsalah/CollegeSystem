using AutoMapper;
using Framework.Core.comman;
using Framework.Core.IUnit;
using Microsoft.Extensions.Logging;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Professor;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorsRepository professorsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<ProfessorService> logger;

        public ProfessorService(IProfessorsRepository professorsRepository
            ,IUnitOfWork unitOfWork,IMapper mapper,ILogger<ProfessorService> logger)
        {
            this.professorsRepository = professorsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        private async Task<Professor> ProfessorValidation(int id)
        {
            var query = await professorsRepository.GetById(id);

            if (query is null)
            {
                logger.LogWarning("No exist Professor with #id: {id}", id);
                throw new ItemNotFoundException("Item not found");
            }

            return query;
        }

        public async Task<IList<ProfessorVM>> GetAll()
        {
            var query= await professorsRepository.GetAll();

            if(query is null || query.Count==0)
            {
                logger.LogWarning("No exist any Professors");
                throw new ItemNotFoundException("Items not found");
            }

            var map= mapper.Map<IList<ProfessorVM>>(query);

            return map;
        }

        public async Task<ProfessorVM> GetById(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            // validate Professor
            var query = await ProfessorValidation(id);

            var map= mapper.Map<ProfessorVM>(query);

            return map;
        }

        public async Task<int> Add(ProfessorVM professor)
        {
            if(professor is null)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map = mapper.Map<Professor>(professor);

            await professorsRepository.AddEntity(map);
            await unitOfWork.SaveChanges();

            return map.Id;
        }

        public async Task Update(UpdateProfessorVM professor)
        {
            logger.LogWarning("Now, you try accessing with #Id: {id}", professor.Id);

            var query= await ProfessorValidation(professor.Id);

            //query.Name = professor.Name;
            //query.Email = professor.Email;
            //query.Phone = professor.Phone;
            //query.DateOfBirth = professor.DateOfBirth;
            //query.DeptId = professor.DeptId;

            mapper.Map(professor, query);

            await unitOfWork.SaveChanges();
        }

        public async Task Delete(int profId)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", profId);

            var query = await ProfessorValidation(profId);

            await professorsRepository.DeleteEntity(query);
            await unitOfWork.SaveChanges();
        }

        public async Task AddCourses(int profId, IList<CourseVM> courses)
        {
            var query = await ProfessorValidation(profId);

            if(courses is null || courses.Count ==0)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map= mapper.Map<IList<Course>>(courses);

            await professorsRepository.AddCourses(query, map);
            await unitOfWork.SaveChanges();
        }

        public async Task<IList<CourseVM>> GetCourses(int profId)
        {
            var query = await ProfessorValidation(profId);

            var courses = await professorsRepository.GetCourses(profId);

            if (courses is null || courses.Count == 0)
            {
                logger.LogWarning("No exist Courses for Professor with ID: {id}", profId);
                throw new ItemNotFoundException("Items not found");
            }

            var map= mapper.Map<IList<CourseVM>>(courses);

            return map;
        }


        public async Task<long> GetNumberOfCourses(int profId)
        {
            var query = await ProfessorValidation(profId);

            var count = await professorsRepository.GetNumberOfCourses(profId);

            if(count==0)
            {
                logger.LogWarning("Number of Courses for Professor with ID: {id} equal zero", profId);
                throw new ItemNotFoundException("Item not found");
            }

            return count;
        }

        public async Task<IList<ProfessorVM>> Paginate(int pageIndex, int pageSize)
        {
            var page= await professorsRepository.Pagination(pageIndex, pageSize);

            if(page is null || page.Count == 0)
            {
                logger.LogWarning("No data to display");
                throw new ItemNotFoundException("Items not found");
            }

            var map= mapper.Map<IList<ProfessorVM>>(page);

            return map;
        }
    }
}
