using Framework.Core.IRepo;
using Framework.Core.IUnit;
using Framework.DataAccess.Interceptors;
using Framework.DataAccess.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.API;
using Service.Business.Services;
using Service.Core.AutoMapper;
using Service.Core.IRepo;
using Service.Core.IServices;
using Service.DataAccess.Context;
using Service.DataAccess.Repo;
using Service.DataAccess.Unit;

namespace Service.DependencyInjection
{
    public static class Container
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {

            // Context
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:Default"]);
                options.AddInterceptors(new SoftDeleteInterceptor());
            });

            // Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // NonGeneric Repository
            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddScoped<IProfessorsRepository,ProfessorRepository>();
            services.AddScoped<IStudentRepository,StudentRepository>();
            services.AddScoped<ICourseRepository,CourseRepository>();
            services.AddScoped<IUserRepository,UserRepository>();

            // Service
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IUserService, UserService>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
    }
}