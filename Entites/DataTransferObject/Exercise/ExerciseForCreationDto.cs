using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.Exercise
{
   public record ExerciseForCreationDto(
       
       int userid,
       string Name,
        string MuscleGroup,
        string Description,
        int? DefaultReps,
        int? DefaultSets
       );
}
