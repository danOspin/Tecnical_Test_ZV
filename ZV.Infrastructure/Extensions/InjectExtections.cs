using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Infrastructure.Persistences.Contexts;
using ZV.Infrastructure.Persistences.Interfaces;
using ZV.Infrastructure.Persistences.Repositories;

namespace ZV.Infrastructure.Extensions
{
    public static class InjectExtections
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DataBaseContext).Assembly.FullName;
            services.AddDbContext<DataBaseContext>(
                options => options.UseSqlServer
                    (configuration.GetConnectionString("DBConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                    );
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
