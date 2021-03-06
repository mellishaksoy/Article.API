﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Article.API.Domain
{
    public class Comment : IEntity, ISoftDelete, IDeleteAuditing, IInsertAuditing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }

        public string Body { get; set; }

        public bool IsDeleted { get; set; }
        public string InsertedUser { get; set; }
        public DateTime? InsertedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Article Article { get; set; }
    }
}
