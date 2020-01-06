using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Article.API.Domain
{
    public class Article : IEntity, ISoftDelete, IDeleteAuditing, IInsertAuditing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public bool IsDeleted { get; set; }
        public string InsertedUser { get; set; }
        public DateTime? InsertedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }

        // todo cascade delete

        public virtual Category Category { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
