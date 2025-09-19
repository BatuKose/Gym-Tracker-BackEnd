using Entites.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.ActivityLevelEnum;
using static Entites.Enums.FitnessGoalEnum;

namespace Entites.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public  User User { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public FitnessGoal fitnessGoal { get; set; }

    }
}
