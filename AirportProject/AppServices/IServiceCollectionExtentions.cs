using AppServices.Profiles;
using AppServices.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection,IConfiguration config)
        {
            serviceCollection.AddScoped<IPointService,PointService>();
            serviceCollection.AddScoped<IEdgeService, EdgeService>();

            serviceCollection.AddScoped<IMainAlgoritmService, MainAlgoritmService>();
            serviceCollection.AddScoped<IDijstraService, DijstraService>();
            serviceCollection.AddScoped<IGraphService, GraphService>();

            //auto mapper בשביל ה
            serviceCollection.AddAutoMapper(typeof(PointProfile));

            serviceCollection.AddRepositories(config);
            return serviceCollection;
        }
    }
}
