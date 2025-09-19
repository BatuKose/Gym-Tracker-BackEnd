using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.User
{
    public class CreateUserDto
    {
        public bool isAdmin { get; set; } = false;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public bool erkekMi { get; set; }
        public string cepTel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
