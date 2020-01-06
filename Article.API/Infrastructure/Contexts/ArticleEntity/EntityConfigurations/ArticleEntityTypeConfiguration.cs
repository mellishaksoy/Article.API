using Article.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.API.Infrastructure.Contexts.ArticleEntity.EntityConfigurations
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Article>
    {
        public void Configure(EntityTypeBuilder<Domain.Article> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(e => e.InsertedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

            builder.Property(e => e.Body)
                    .IsRequired();
            builder.Property(e => e.CategoryId)
                    .IsRequired();
            builder.Property(e => e.InsertedUser)
                    .IsRequired();

        }
    }
}