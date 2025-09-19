using Entites.Models;
using Entites.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserRepository:IrepositoryBase<User>
    {
       Task< IEnumerable<User>> GetAllUserAsync(UserParameters userParameters, bool trackChanges);
        Task<User>GetUserByIdAsync(int id, bool trackChanges);

        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User GetUserWithExercises(UserWithExerciseParameters userWithExerciseParameters,int userId);
    }
}
