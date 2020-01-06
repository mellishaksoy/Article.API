using Article.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.API.Infrastructure.Contexts.ArticleEntity.EntityConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(e => e.InsertedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

            builder.Property(e => e.Body)
                    .IsRequired();

            builder.Property(e => e.InsertedUser)
                   .IsRequired();

        }
    }
}
