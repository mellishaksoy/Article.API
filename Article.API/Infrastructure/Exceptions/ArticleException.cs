using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Exceptions
{
    [Serializable]
    public class ArticleException : BaseException<ArticleErrorCodes>
    {
        public ArticleException(ArticleErrorCodes code) : base(code)
        {
        }

        public ArticleException(ArticleErrorCodes code, object model) : this(code)
        {
        }

        public ArticleException(ArticleErrorCodes code, string message, params object[] args) : base(code, message, args)
        {
        }
    }
}
