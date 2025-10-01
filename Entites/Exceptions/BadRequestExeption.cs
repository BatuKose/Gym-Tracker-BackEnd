using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class BadRequestExeption : CustomExeption
    {
        public BadRequestExeption(string message) : base(message, (int)HttpStatusCode.BadRequest){}
    }
}
