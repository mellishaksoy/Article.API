using System;

namespace Article.API.Application.Dtos.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime InsertedDate { get; set; }
    }
}
