using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSLatam.Common.Application;
using IDSLatam.Service.MiApi.Application.Repositories;
using IDSLatam.Service.MiApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IDSLatam.Service.MiApi.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable("__EFMigrationHistory", "abc")
                    )
                );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ITest, TestRepository>();
             

            return services; 
    }}
}