using Entites.Models;
using Entites.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public  sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {

        }
        
        public void CreateUser(User user)=>Create(user);
      

        public void DeleteUser(User user)=>Delete(user);


        public async Task< IEnumerable<User>> GetAllUserAsync(UserParameters userParameters,bool trackChanges)
        {
            
            return !trackChanges
               ? await _context.Users.AsNoTracking().Search(userParameters.SearchTerm).OrderBy(u=>u.Id).Skip((userParameters.PageNumber-1)*userParameters.PageSize).Take(userParameters.PageSize).ToListAsync()  
                :  await _context.Users.ToListAsync();                 
        }
      



        public async Task< User> GetUserByIdAsync(int id, bool trackChanges) => await FinAllByCondition(u => u.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public User GetUserWithExercises(UserWithExerciseParameters  userWithExerciseParameters,int userId)
        {
            return _context.Users.Include(x => x.Exercises)
                .Search(userWithExerciseParameters.SearchTerm)
                .OrderBy(x=>x.Id).Skip((userWithExerciseParameters.PageNumber-1)*userWithExerciseParameters.PageSize)
                .Take(userWithExerciseParameters.PageSize).FirstOrDefault(x=>x.Id==userId);
        }

        public void UpdateUser(User user) => Update(user);
        
    }
}
