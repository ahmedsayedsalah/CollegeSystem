using AutoMapper;
using Framework.Core.comman;
using Framework.Core.IUnit;
using Microsoft.Extensions.Logging;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.Core.Models.ViewModels;
using Service.Core.Models.ViewModels.Department;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ILogger<DepartmentService> logger;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentService(ILogger<DepartmentService> logger,IDepartmentRepository departmentRepository
            ,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.logger = logger;
            this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        private async Task<Department> DepartmentValidation(int id)
        {
            var query = await departmentRepository.GetById(id);

            if (query is null)
            {
                logger.LogWarning("No exist Department with #id: {id}", id);
                throw new ItemNotFoundException("Item not found");
            }

            return query;
        }

        public async Task<IList<DepartmentVM>> GetAll()
        {
            var query= await departmentRepository.GetAll();

            if(query is null || query.Count == 0)
            {
                logger.LogWarning($"No exist Departments");
                throw new ItemNotFoundException("Items are not found");
            }

            var map= mapper.Map<List<DepartmentVM>>(query);
            return map;
        }

        public async Task<DepartmentVM> GetById(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            var query= await DepartmentValidation(id);

            var map= mapper.Map<DepartmentVM>(query);
            return map;
        }
        public async Task<int> Add(DepartmentVM department)
        {
            if(department is null)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map= mapper.Map<Department>(department);

            await departmentRepository.AddEntity(map);
            await unitOfWork.SaveChanges();

            return map.Id;
        }

        public async Task Delete(int id)
        {
            logger.LogDebug("Now, you try accessing with #Id: {id}", id);

            var query = await DepartmentValidation(id);

            await departmentRepository.DeleteEntity(query);
            await unitOfWork.SaveChanges();
        }

        public async Task Update(UpdateDepartmentVM department)
        {
            logger.LogWarning("Now, you try accessing with #Id: {id}", department.Id);

            var query = await DepartmentValidation(department.Id);

            //query.Title = department.Title;
            //query.Description = department.Description;

            mapper.Map(department,query);
            
            await unitOfWork.SaveChanges();
            
        }

        public async Task AddProfessors(int deptId, List<ProfessorVM> professors)
        {
            var query = await DepartmentValidation(deptId);

            if(professors is null || professors.Count==0)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map= mapper.Map<List<Professor>>(professors);

            await departmentRepository.AddProfessors(query, map);
            await unitOfWork.SaveChanges();
        }

        public async Task AddCourses(int deptId, List<CourseVM> courses)
        {
            var query = await DepartmentValidation(deptId);

            if (courses is null || courses.Count == 0)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map= mapper.Map<List<Course>>(courses);

            await departmentRepository.AddCourses(query, map);
            await unitOfWork.SaveChanges();
        }

        public async Task AddStudents(int deptId, List<StudentVM> students)
        {
            var query = await DepartmentValidation(deptId);

            if (students is null || students.Count == 0)
            {
                logger.LogWarning("Data can not be null");
                throw new BadRequestException("Bad data sent");
            }

            var map = mapper.Map<List<Student>>(students);

            await departmentRepository.AddStudents(query, map);
            await unitOfWork.SaveChanges();
        }
        public async Task<IList<ProfessorVM>> GetProfessors(int deptId)
        {
            var query = await DepartmentValidation(deptId);

            var professors = await departmentRepository.GetProfessors(deptId);

            if(professors is null || professors.Count == 0)
            {
                logger.LogWarning("No exist Professors for Department with ID: {id}", deptId);
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<List<ProfessorVM>>(professors);

            return map;
        }

        public async Task<IList<CourseVM>> GetCourses(int deptId)
        {
            var query = await DepartmentValidation(deptId);

            var courses= await departmentRepository.GetCourses(deptId);

            if(courses is null || courses.Count == 0)
            {
                logger.LogWarning("No exist Courses for Department with ID: {id}", deptId);
                throw new ItemNotFoundException("Items not found");
            }
            var map= mapper.Map<List<CourseVM>>(courses);

            return map;
        }

        public async Task<IList<StudentVM>> GetStudents(int deptId)
        {
            var query = await DepartmentValidation(deptId);

            var students = await departmentRepository.GetStudents(deptId);

            if(students is null || students.Count == 0)
            {
                logger.LogWarning("No exist Students for Department with ID: {id}", deptId);
                throw new ItemNotFoundException("Items not found");
            }

            var map = mapper.Map<List<StudentVM>>(students);

            return map;
        }

        public async Task<IList<string>> GetNamesOfCourses(int deptId)
        {
            var query = await DepartmentValidation(deptId);

            var names = await departmentRepository.GetNamesOfCourses(deptId);

            if(names is null || names.Count == 0)
            {
                logger.LogWarning("No exist names of Courses for Department with ID: {id}", deptId);
                throw new ItemNotFoundException("Items not found");
            }

            return names;
        }

        public async Task<long> GetNumberOfStudents(int deptId)
        {
            var query = await DepartmentValidation(deptId);

            var count= await departmentRepository.GetNumberOfStudents(deptId);

            if(count == 0)
            {
                logger.LogWarning("Number of Students for Department with ID: {id} equal zero", deptId);
                throw new ItemNotFoundException("Item not found");
            }

            return count;
        }
    }
}
