using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.UserProfile
{
    public class UserProfileDto
    {
        // User
        public string UserName{ get; set; }
        public bool Erkekmi { get; set; }
       
        // profile
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public string ActivityLevel { get; set; } 
        public string FitnessGoal { get; set; }
    }
}
