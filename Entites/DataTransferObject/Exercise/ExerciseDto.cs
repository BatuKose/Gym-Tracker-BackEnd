using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.Exercise
{
    // Frontend’e gönderilen veri
    public record ExerciseDto(
        int Id,
        string Name,
        string MuscleGroup,
        string Description,
        string? DefaultReps,
        string? DefaultSets
        );
    
}
