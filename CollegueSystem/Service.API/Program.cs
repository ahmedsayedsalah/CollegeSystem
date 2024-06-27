using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Service.API;
using Service.API.Middlewares;
using Service.API.Security.Authentication;
using Service.DependencyInjection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Container.Register(builder.Services, builder.Configuration);

// call the UseSerilog function in the HostBuilder instance to configure Serilog
builder.Host.UseSerilog((context, config) =>
config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JwtOptions
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

// initialze
var jwtOptions = builder.Services.BuildServiceProvider()
    .GetRequiredService<IOptionsMonitor<JwtOptions>>();

// Authentication
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer= true,
            ValidIssuer= jwtOptions.CurrentValue.Issure,
            ValidateAudience= true,
            ValidAudience= jwtOptions.CurrentValue.Audience,
            ValidateIssuerSigningKey= true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.CurrentValue.SigningKey))
        };
    });

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DepartmentViewPolicy", builder =>
    {
        builder.RequireRole("Administrator", "DepartmentHead");
    });
    options.AddPolicy("DepartmentManagePolicy", builder =>
    {
        builder.RequireRole("Administrator");
    });

    options.AddPolicy("ProfessorViewPolicy", builder =>
    {
        builder.RequireRole("Administrator", "DepartmentHead");
    });
    options.AddPolicy("ProfessorManagePolicy", builder =>
    {
        builder.RequireRole("Administrator");
    });

    options.AddPolicy("StudentViewPolicy", builder =>
    {
        builder.RequireRole("Administrator", "DepartmentHead", "Professor");
    });
    options.AddPolicy("StudentManagePolicy", builder =>
    {
        builder.RequireRole("Administrator", "Professor");
    });

    options.AddPolicy("CourseViewPolicy", builder =>
    {
        builder.RequireRole("Administrator", "DepartmentHead", "Professor", "Student");
    });
    options.AddPolicy("CourseManagePolicy", builder =>
    {
        builder.RequireRole("Administrator", "Professor");
    });

    options.AddPolicy("UserPolicy", builder =>
    {
        builder.RequireRole("Administrator");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
