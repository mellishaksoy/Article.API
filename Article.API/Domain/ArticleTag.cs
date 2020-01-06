using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Article.API.Domain
{
    public class ArticleTag : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        [ForeignKey("Tag")]
        public Guid TagId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
