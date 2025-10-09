using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject
{
    public record class userForAuthenticationDto
    {
        [Required(ErrorMessage ="Kullanıcı adı girmek zorunludur")]
        public string? userName { get; init; }
        [Required(ErrorMessage ="Şifre girmek zorunludur")]
        public string? PassWord { get; init; }
    }
}
