using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Article.API.Domain
{
    public class Category : IEntity, ISoftDelete, IDeleteAuditing, IInsertAuditing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        public string InsertedUser { get; set; }
        public DateTime? InsertedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual  ICollection<Article> Articles { get; set; }
    }
}
