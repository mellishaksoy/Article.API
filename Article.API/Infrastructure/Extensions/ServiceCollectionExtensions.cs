using Article.API.Application.Services.Article;
using Article.API.Application.Services.Category;
using Article.API.Application.Services.Comment;
using Article.API.Application.Services.Tag;
using Article.API.Domain;
using Article.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Article.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            serviceCollection.AddTransient<ITagRepository, TagRepository>();
            serviceCollection.AddTransient<ITagService, TagService>();

            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();

            serviceCollection.AddTransient<IArticleRepository, ArticleRepository>();
            serviceCollection.AddTransient<IArticleService, ArticleService>();

            serviceCollection.AddTransient<ICommentRepository, CommentRepository>();
            serviceCollection.AddTransient<ICommentService, CommentService>();
        }
        public static void ConfigureDatabase(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
        }

    }
}
