using Article.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Contexts.ArticleEntity
{
    public class ArticleContextSeed
    {
        public enum LanguageCode
        {
            EN = 0,
            TR = 1
        }
        

        public async Task SeedAsync(DbContext context)
        {
            var dbContext = context.GetService<ArticlesContext>();
            if (!dbContext.Categories.Any())
            {
                await dbContext.AddRangeAsync(CategoryObjectsForSuccessCreate);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Tags.Any())
            {
                await dbContext.AddRangeAsync(TagObjectsForSuccessCreate);
                await dbContext.SaveChangesAsync();
            }
        }

        public static List<Category> CategoryObjectsForSuccessCreate =>
           new List<Category>
           {
               new Category
               {
                   Id = new Guid("7dca9794-bfdb-41c2-bac6-5b4371ac6c26"),
                   Name = "Education"
               },
               new Category
               {
                   Id = new Guid("65d6f702-6a72-4814-b40a-7f4f72f9471d"),
                   Name = "Novel"
               },
               new Category
               {
                   Id = new Guid("a90fd3f2-ce26-4417-a78b-75b7bc98bbe9"),
                   Name = "Adventure"
               }
           };

        public static List<Tag> TagObjectsForSuccessCreate =>
           new List<Tag>
           {
               new Tag
               {
                   Id = new Guid("3d343607-0112-488a-92a8-37ddc99366f1"),
                   Name = "Funny"
               },
               new Tag
               {
                   Id = new Guid("a58197d8-0922-4e2e-9268-e4f98ec0bad1"),
                   Name = "Novel"
               },
               new Tag
               {
                   Id = new Guid("a90fd3f2-ce26-4417-a78b-75b7bc98bbe9"),
                   Name = "Fluent"
               }
           };
    }
}
