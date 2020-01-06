using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Application.Dtos.Tag
{
    public class TagUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
