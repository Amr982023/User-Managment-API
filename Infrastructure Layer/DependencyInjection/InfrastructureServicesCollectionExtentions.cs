using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Interfaces.Security;
using Domain_Layer.Interfaces;
using Domain_Layer.Interfaces.IUOW;
using Infrastructure_Layer.Repositories.JWT;
using Infrastructure_Layer.Repositories.Repos;
using Infrastructure_Layer.Repositories.Security;
using Infrastructure_Layer.Repositories.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure_Layer.DependencyInjection
{
    public static class InfrastructureServicesCollectionExtentions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddDbContext<UserManagmentDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(UserManagmentDbContext).Assembly.GetName().Name)));
            
            services.AddScoped<IUser , UserRepository>();

            services.AddScoped<IUnitOfWork , UnitOfWork>();

            services.AddScoped<IPasswordHasher , PasswordHasher>();

            services.AddScoped<ITokenGenerator , TokenGenerator>();


            return services;
        }
    }
}
