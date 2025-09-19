using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class User
    {
        public int Id { get; set; }  // PK
        [Required(ErrorMessage ="Kullanıcı yetki seçimi yapınız")]
        public bool isAdmin { get; set; }=false;
        [Required(ErrorMessage ="Kullanıcı adı boş olamaz")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage ="E posta boş olamaz")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password boş olamaz")]
        public string PasswordHash { get; set; } = null!;
        [Required(ErrorMessage = "Doğum Tarihi boş olamaz")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Cinsiyet seçimi boş olamaz")]
        public bool erkekMi { get; set; }
        [Required(ErrorMessage ="Cep telefonu giriniz")]
        public string cepTel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Exercise> Exercises { get; set; }
    }
}
