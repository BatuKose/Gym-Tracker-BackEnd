using Entites.DataTransferObject.Exercise;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IExerciseService
    {
       
        public Task CreateExercise(ExerciseForCreationDto dto,int UserId);
        public Task UpdateExercise(ExerciseForUpdateDto exerciseDto,int id);
        GetExerciseDto GetExerciseById(int id, bool trackChanges);
        public void DeleteExerciseById(int id);
    }
}
