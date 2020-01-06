using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface IInsertAuditing
    {
        string InsertedUser { get; set; }
        DateTime? InsertedDate { get; set; }
    }
}
