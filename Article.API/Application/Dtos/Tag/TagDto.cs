﻿using System;

namespace Article.API.Application.Dtos.Tag
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public DateTime InsertedDate { get; set; }
    }
}
