using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetUserProgileAsync(int id, bool trackChanges);
    
    }
}
