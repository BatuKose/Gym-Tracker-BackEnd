using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class Exercise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                  
        public string Name { get; set; }            
        public string MuscleGroup { get; set; }  
        public string Description { get; set; }     
        public int? DefaultReps { get; set; }       
        public int? DefaultSets { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }

}
