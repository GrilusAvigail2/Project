
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Repositories
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddScoped<IPointRepository, PointRepository>();
            serviceCollection.AddScoped<IEdgeRepository, EdgeRepository>();
            serviceCollection.AddDbContext<MainDataContext>(opt => opt.UseSqlServer(config.GetConnectionString("MainDataConnection")));
            return serviceCollection;
        }
    }
}
 