using Application.Core;
using Application.Interfaces;
using Application.ThematicAreas;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Ejaz.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;
                if (env == "Development")
                {       
                    connStr = config.GetConnectionString("DefaultConnection");
                } else
                {
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                
            
            options.UseSqlServer(connStr, options => { options.CommandTimeout(3000); });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
               {
                   policy
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                        .WithOrigins("http://localhost:3011")
                        .WithOrigins("http://localhost:3000")
                        //.WithOrigins("https://ejazclient.azurewebsites.net");
                        //.WithOrigins("https://ejazclientapp.azurewebsites.net");
                        .WithOrigins("http://localhost:3011","https://ejaz-api.azurewebsites.net", "https://ejaz-adminpanel.azurewebsites.net");
               });
            });
            services.AddMediatR(typeof(List.Handler));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}