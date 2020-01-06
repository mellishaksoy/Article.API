using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Domain
{
    public interface IDeleteAuditing
    {
        string DeletedUser { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
