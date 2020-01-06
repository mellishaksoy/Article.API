using Article.API.Infrastructure.Contexts.ArticleEntity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Extensions
{
    public static class SeedDataExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            var serviceScopeFactory = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ArticlesContext>();
                new ArticleContextSeed().SeedAsync(dbContext).Wait();

            }

            return host;
        }
    }
}
