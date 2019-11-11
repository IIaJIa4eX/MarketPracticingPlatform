using MarketServicesDataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Services
{
    public  static class DBConnectService
    {
        public static void AddConnectService(this IServiceCollection services)
        {
            services.AddScoped<GetDbData>();
        }
    }
}
