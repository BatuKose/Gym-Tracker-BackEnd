using Entites.DataTransferObject.ExerciseWithUser;
using Entites.DataTransferObject.User;
using Entites.Models;
using Entites.RequestFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Services.Contracts
{
    public interface IUserService
    {
        public Task CreateUser(User user);
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync(UserParameters userParameters,bool trackChanges);
         Task< User> GetUserByIdAsync(int id,bool trackChanges);
        public Task DeleteUser(int id, bool trackChanges);
        public Task UpdateUser(int id, User user, bool trackChanges);
        UserWithExercisesDto GetUserWithExercises(UserWithExerciseParameters userWithExerciseParameters,int id);
        Task<List<User>> GetAllUsersAsync(bool trackChanges);
      
    }
}
