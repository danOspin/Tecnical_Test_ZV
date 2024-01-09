using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZV.Application.Interfaces;
using ZV.Application.Services;

namespace ZV.Application.Extensions
{
    public static class InjectExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton(configuration);
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserInfoApplication, UserInfoApplication>();
            services.AddScoped<ICommerceApplication, CommerceApplication>();
            services.AddScoped<ITransactionApplication, TransactionApplication>();
            services.AddScoped<IClientApplication, ClientApplication>();

            return services;
        }
    }
}
