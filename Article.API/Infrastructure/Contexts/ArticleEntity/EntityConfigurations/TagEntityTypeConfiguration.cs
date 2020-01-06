using Article.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.API.Infrastructure.Contexts.ArticleEntity.EntityConfigurations
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(e => e.InsertedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

            builder.Property(e => e.Name)
                    .IsRequired();

            builder.Property(e => e.InsertedUser)
                   .IsRequired();
        }
    }
}
