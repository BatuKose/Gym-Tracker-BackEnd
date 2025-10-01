using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class DublicateExeptions : CustomExeption
    {
        public DublicateExeptions(string message) : base(message,(int)HttpStatusCode.Conflict){}
    }
}
