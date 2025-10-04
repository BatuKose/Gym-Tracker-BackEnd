using Entites.DataTransferObject.UserProfile;
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
        Task<UserProfileDto> GetUserProfileAsync(int id, bool trackChanges);
       
    }
}
