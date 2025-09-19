using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IExerciseRepository
    {
        void CreateExercise(Exercise exercise);
        void UpdateExercise(Exercise exercise);
        Exercise GetExerciseByid(int id, bool trackChanges);
        void DeleteExercise(Exercise exercise);
    }
}
