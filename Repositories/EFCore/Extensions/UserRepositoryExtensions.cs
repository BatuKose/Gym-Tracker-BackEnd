using Entites.DataTransferObject.ExerciseWithUser;
using Entites.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

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
        public static IQueryable<User> Sort(this IQueryable<User> users, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return users.OrderBy(u => u.Id);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return users.OrderBy(u => u.Id);
            return users.OrderBy(orderQuery); 
        }
    }
}
