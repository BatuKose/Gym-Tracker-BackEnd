using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfile> GetUserProfileAsync(int id, bool trackChanges);
    }
}
