using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public class UserNotFoundExeption:CustomExeption
    {
        public UserNotFoundExeption(): base("Kullanıcı bulunamadı", (int)HttpStatusCode.NotFound) { }
    }
}
