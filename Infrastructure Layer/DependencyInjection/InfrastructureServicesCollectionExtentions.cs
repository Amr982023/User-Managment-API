using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return services;
        }
    }
}
