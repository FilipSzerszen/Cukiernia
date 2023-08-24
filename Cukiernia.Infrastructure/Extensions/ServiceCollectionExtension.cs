using Cukiernia.Domain.Interfaces;
using Cukiernia.Infrastructure.Persistence;
using Cukiernia.Infrastructure.Repositories;
using Cukiernia.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CukierniaDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("CukierniaDb")));

            services.AddDefaultIdentity<IdentityUser>(options => {
                options.Stores.MaxLengthForKeys = 450;
            })
            .AddEntityFrameworkStores<CukierniaDbContext>();

            services.AddScoped<CukierniaSeeder>();

            services.AddScoped<IBakingRepository, BakingRepository>();
        }
    }
}
