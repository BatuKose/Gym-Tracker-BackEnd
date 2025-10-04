using Entites.DataTransferObject.UserProfile;
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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly RepositoryContext _context;

        public UserProfileRepository(RepositoryContext context)
        {
            _context=context;
        }

       

     

        public async Task<UserProfile> GetUserProgileAsync(int id, bool trackChanges)
        {
                return trackChanges
                ? await _context.UserProfiles.Include(u => u.User).FirstOrDefaultAsync(x => x.UserId == id)
                : await _context.UserProfiles.AsNoTracking().Include(u => u.User).FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}
