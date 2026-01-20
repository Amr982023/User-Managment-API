using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Interfaces;
using Application_Layer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer.DependencyInjection
{
    public static class ApplicationServicesCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }
    }
}
