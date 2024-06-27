using AutoMapper;
using Framework.Core.comman;
using Framework.Core.IUnit;
using Microsoft.Extensions.Logging;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Course;
using Service.Entities.Entity;
using InvalidOperation = Framework.Core.comman.InvalidOperationException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository courseRepository;
        private IStudentRepository studentRepository;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILogger<CourseService> logger;

        public CourseService(ICourseRepository courseRepository,IStudentRepository studentRepository
            , IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseService> logger)
        {
            this.courseRepository = courseRepository;
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        private async Task<Course> CourseValidation(int id)
        {
            var query = await courseRepository.GetById(id);

            if (query is null)
            {
                logger.LogWarning("No exist Course with #id: {id}", id);
                throw new ItemNotFoundException("Item not found");
            }

            return query;
        }
        public async Task<IList<CourseVM>> GetAll()
        {
            var query = await courseRepository.GetAll();

            if (query is null || query.Count == 0)
            {
                logger.LogWarning("No exist any Students");
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<IList<CourseVM>>(query);

            return map;
        }

        public async Task<IList<CourseVM>> Paginate(int pageIndex, int pageSize)
        {
            var page = await courseRepository.Pagination(pageIndex, pageSize);

            if (page is null || page.Count == 0)
            {
                logger.LogWarning("No data to display");
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<IList<CourseVM>>(page);

            return map;
        }

        public async Task<CourseVM> GetById(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            var query = await CourseValidation(id);

            var map = mapper.Map<CourseVM>(query);

            return map;
        }
        public async Task<int> Add(CourseVM course)
        {
            if (course is null)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map = mapper.Map<Course>(course);

            await courseRepository.AddEntity(map);
            await unitOfWork.SaveChanges();

            return map.Id;
        }

        public async Task Update(UpdateCourseVM course)
        {
            logger.LogWarning("Now, you try accessing with #Id: {id}", course.Id);

            var query = await CourseValidation(course.Id);

            mapper.Map(course, query);

            await unitOfWork.SaveChanges();
        }
        public async Task Delete(int crsId)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", crsId);

            var query = await CourseValidation(crsId);

            await courseRepository.DeleteEntity(query);
            await unitOfWork.SaveChanges();
        }

        public async Task AddStudents(int crsId, IList<int> students)
        {
            // validate course
            var course= await CourseValidation(crsId);
           
            // validatation of students
            var existStudents= await studentRepository.GetWithCondition(x=> students.Contains(x.Id));
            var nonExistStudents= students.Except(existStudents.Select(x=> x.Id)).ToList();
            if (nonExistStudents.Any())
            {
                throw new InvalidOperation($"One more Students with the following IDs do not exist: {string.Join(" ,", nonExistStudents)}");
            }

            await courseRepository.AddStudents(course, students);
            await unitOfWork.SaveChanges();
        }

        public async Task<IList<StudentVM>> GetStudents(int crsId)
        {
            // validate course
            var course = await CourseValidation(crsId);

            var students= await courseRepository.GetStudents(course);

            if(students is null || students.Count == 0)
            {
                logger.LogWarning("No exist any Students for This Course with ID: {id}", crsId);
                throw new ItemNotFoundException("Item not found");
            }

            var map = mapper.Map<IList<StudentVM>>(students);

            return map;
        }

    }
}
