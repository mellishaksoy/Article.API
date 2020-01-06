using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article.API.Domain;
using Article.API.Infrastructure.Contexts.ArticleEntity.EntityConfigurations;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Article.API.Infrastructure.Contexts.ArticleEntity
{
    public class ArticlesContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticlesContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Domain.Article> Articles { get; set; }
        public DbSet<Domain.ArticleTag> ArticleTags { get; set; }
        public DbSet<Domain.Category> Categories { get; set; }
        public DbSet<Domain.Comment> Comments { get; set; }
        public DbSet<Domain.Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if(entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "IsDeleted") != null)
                            entry.CurrentValues["IsDeleted"] = false;
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "InsertedUser") != null)
                            entry.CurrentValues["InsertedUser"] = userId ?? "articleAPIUser";
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "InsertedDate") != null)
                            entry.CurrentValues["InsertedDate"] = DateTime.Now;

                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "IsDeleted") != null)
                            entry.CurrentValues["IsDeleted"] = true;
                        else
                        {
                            entry.State = EntityState.Deleted;
                        }
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "DeletedUser") != null)
                            entry.CurrentValues["DeletedUser"] = userId;
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "DeletedDate") != null)
                            entry.CurrentValues["DeletedDate"] = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "UpdatedUser") != null)
                            entry.CurrentValues["UpdatedUser"] = userId;
                        if (entry.CurrentValues.Properties.FirstOrDefault(x => x.Name == "UpdatedDate") != null)
                            entry.CurrentValues["UpdatedDate"] = DateTime.Now;
                        break;
                }
            }
        }
    }
}
