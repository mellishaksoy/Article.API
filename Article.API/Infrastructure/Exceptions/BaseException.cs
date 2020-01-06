using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article.API.Infrastructure.Exceptions
{
    public class BaseException<T> : Exception
    {
        public T Code { get; set; }
        public string SerializedModel { get; set; }
        public object[] Arguments { get; set; }


        public BaseException()
        {

        }

        public BaseException(T code)
        {
            Code = code;
        }
        

        public BaseException(T code, params object[] arguments) : this(code)
        {
            Arguments = arguments;
        }

        public BaseException(string message, params object[] args) : this(default(T), message, args)
        {

        }

        public BaseException(T code, string message, params object[] args) : this(null, code, message, args)
        {

        }

        public BaseException(Exception innerException, string message, params object[] args)
            : this(innerException, default(T), message, args)
        {

        }

        public BaseException(Exception innerException, T code)
            : base(null, innerException)
        {
            Code = code;
        }

        public BaseException(Exception innerException, T code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
