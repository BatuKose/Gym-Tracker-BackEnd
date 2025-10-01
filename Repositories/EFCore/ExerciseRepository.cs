using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ExerciseRepository : IExerciseRepository
    {

        private readonly RepositoryContext _context;

        public ExerciseRepository(RepositoryContext context)
        {
            _context = context;
        }

        public void CreateExercise(Exercise exercise) => _context.Exercise.Add(exercise);

        public void DeleteExercise(Exercise exercise)=>_context.Exercise.Remove(exercise);
        

        public Exercise GetExerciseByid(int id, bool trackChanges)
        {
            return trackChanges
                ? _context.Exercise
                    .Include(e => e.User)        // ← User bilgisiyle getir
                    .FirstOrDefault(e => e.Id == id)
                : _context.Exercise
                    .AsNoTracking()
                    .Include(e => e.User)        // ← User bilgisiyle getir
                    .FirstOrDefault(e => e.Id == id);
        }

        //public Exercise GetExerciseForUpdate(int id, int set, int tekrar, string name)
        //{
        //    return _context.Exercise.FirstOrDefault(e =>
        //        e.Id == id &&
        //        e.Name == name &&
        //        e.DefaultSets == set &&
        //        e.DefaultReps == tekrar);
        //}


        public void UpdateExercise(Exercise exercise)=>_context.Exercise.Update(exercise);
       
    }
}
