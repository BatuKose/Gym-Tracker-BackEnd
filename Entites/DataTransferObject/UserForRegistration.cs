using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject
{
    public record UserForRegistration
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string? userName { get; init; }
        [Required(ErrorMessage = "şifre zorunludur")]
        public string? passWord { get; init; }
        public string? Email { get; init; }
        public string? phoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }


    }
}
