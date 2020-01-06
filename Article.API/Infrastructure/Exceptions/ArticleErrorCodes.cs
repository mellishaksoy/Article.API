using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Exceptions
{
    public enum ArticleErrorCodes
    {
        TagIdCannotBeNull = 100,
        TagCouldNotBeFound = 101,
        TagNameCannotBeNull = 102,
        CategoryIdCannotBeNull = 103,
        CategoryCouldNotBeFound = 104,
        CategoryNameCannotBeNull = 105,
        ArticleTitleCannotBeNull = 106,
        ArticleBodyCannotBeNull = 107,
        ArticleCategoryCannotBeNull = 108,
        ArticleIdCannotBeNull = 109,
        ArticleCouldNotBeFound = 110,
        CommentArticleIdConnotBeNull = 111,
        CommentIdCannotBeNull = 112,
        CommentCouldNotBeFound = 113,
        CommentBodyCannotBeNull = 114,
        TagCannotBeDelete = 115
    }
}
