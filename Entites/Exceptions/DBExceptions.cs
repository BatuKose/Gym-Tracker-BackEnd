using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public sealed class DBExceptions : Exception
    {
        public DBExceptions(string message) : base(message)
        {

        }
    }
}
