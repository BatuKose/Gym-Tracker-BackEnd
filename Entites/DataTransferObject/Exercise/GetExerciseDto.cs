using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.Exercise
{
    public class GetExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string Description { get; set; }
        public int? DefaultReps { get; set; }
        public int? DefaultSets { get; set; }
        public int UserId { get; set; }

    }
}
