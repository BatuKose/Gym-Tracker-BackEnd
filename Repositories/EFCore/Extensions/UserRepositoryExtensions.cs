using Entites.DataTransferObject.ExerciseWithUser;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class UserRepositoryExtensions 
    {
        public static IQueryable<User>Search(this IQueryable<User> users,string SearchTerm)
        {
            if(string.IsNullOrWhiteSpace(SearchTerm)) return users;
            var lowerCase = SearchTerm.Trim().ToLower();
            return users.Where(u => u.Username.Contains(lowerCase));
        }

        public static IQueryable<UserWithExercisesDto>SearchExercise(this IQueryable<UserWithExercisesDto> dto,string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm)) return dto;
            var lowerCase=searchTerm.Trim().ToLower();
            return dto.Where(e => e.Exercise.Any(ex => ex.Name.ToLower().Contains(searchTerm)));
        }
        public static IQueryable<UserWithExercisesDto>Filter(this IQueryable<UserWithExercisesDto> dto, uint minSet,uint maxSet)
        {
            if (minSet==0 || maxSet==0) return dto;
            return dto.Where(e => e.Exercise.Any(ex => ex.DefaultSets>=minSet && ex.DefaultSets<=maxSet));
        }
        public static IQueryable<User>GenderFilter(this IQueryable<User> users,bool? Gender)
        {
            if(Gender.HasValue)
            {
                users.Where(u => u.erkekMi==Gender.Value); 

            }
            return users;
        }
    }
}
