using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DataTransferObject.ExerciseWithUser
{
    public class UserWithExercisesDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public List<ExerciseDtoo> Exercise { get; init; } = new List<ExerciseDtoo>();
    }
}

