using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public abstract class CustomExeption : Exception
    {
        public int StatusCode { get; set; }
        protected CustomExeption(string message,int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
