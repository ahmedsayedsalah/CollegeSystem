using AutoMapper;
using Framework.Core.comman;
using Framework.Core.IUnit;
using Microsoft.Extensions.Logging;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Student;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class StudentService : IStudentService
    {

        private IStudentRepository studentRepository;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILogger<StudentService> logger;

        public StudentService(IStudentRepository studentRepository
            , IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        private async Task<Student> StudentValidation(int id)
        {
            var query = await studentRepository.GetById(id);

            if (query is null)
            {
                logger.LogWarning("No exist Student with #id: {id}", id);
                throw new ItemNotFoundException("Item not found");
            }

            return query;
        }

        public async Task<IList<StudentVM>> GetAll()
        {
            var query = await studentRepository.GetAll();

            if (query is null || query.Count == 0)
            {
                logger.LogWarning("No exist any Students");
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<IList<StudentVM>>(query);

            return map;
        }

        public async Task<IList<StudentVM>> Paginate(int pageIndex, int pageSize)
        {
            var page = await studentRepository.Pagination(pageIndex, pageSize);

            if (page is null || page.Count == 0)
            {
                logger.LogWarning("No data to display");
                throw new ItemNotFoundException("Items not found");
            }

            var map= mapper.Map<IList<StudentVM>>(page);

            return map;
        }

        public async Task<StudentVM> GetById(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            var query= await StudentValidation(id);

            var map= mapper.Map<StudentVM>(query);

            return map;
        }

        public async Task<int> Add(StudentVM student)
        {
            if (student is null)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map = mapper.Map<Student>(student);

            await studentRepository.AddEntity(map);
            await unitOfWork.SaveChanges();

            return map.Id;
        }

        public async Task Update(UpdateStudentVM student)
        {
            logger.LogWarning("Now, you try accessing with #Id: {id}", student.Id);

            var query = await StudentValidation(student.Id);

            mapper.Map(student, query);

            await unitOfWork.SaveChanges();
        }

        public async Task Delete(int stdId)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", stdId);

            var query = await StudentValidation(stdId);

            await studentRepository.DeleteEntity(query);
            await unitOfWork.SaveChanges();
        }

        public async Task EnrollCourse(int stdId, int crsId)
        {
            var query = await StudentValidation(stdId);

            await studentRepository.EnrollCourse(query, crsId);
            await unitOfWork.SaveChanges();
        }

        public async Task<IList<string>> GetEnrolledCourses(int stdId)
        {
            var query = await StudentValidation(stdId);

            return await studentRepository.GetEnrolledCourses(query); 
        }

        public async Task<long> GetNumberOfEnrolledCourses(int stdId)
        {
            var query = await StudentValidation(stdId);

            return await studentRepository.GetNumberOfEnrolledCourses(query);
        }
    }
}
