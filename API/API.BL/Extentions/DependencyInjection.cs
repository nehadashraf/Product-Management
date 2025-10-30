using API.BL.Managers.Product;
using API.DAL.Repositories.ProductRepo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace API.BL.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            return services;
        }
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManger>();
            return services;
        }

    }
}
