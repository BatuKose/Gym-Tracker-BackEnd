using AutoMapper;
using Entites.DataTransferObject.UserProfile;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserProfileManager : IUserProfileService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public UserProfileManager(IRepositoryManager manager, ILoggerService logger,IMapper mapper)
        {
            _manager=manager;
            _logger=logger;
            _mapper=mapper;
        }
        
        // HATALI DTO USER BİLGİSİNDE SIKINTI VAR YARIM KOD
        public async Task<UserProfile> GetUserProfileAsync(int id, bool trackChanges)
        {
            var Profile= await _manager.userProfileRepository.GetUserProgileAsync(id, trackChanges);
            if (Profile is null) throw new Exception("Profil bilgileri bulunamamıştır.");
            var dto = new UserProfileDto()
            {
                
                Weight= Profile.Weight,
                Height= Profile.Height,
                Age= Profile.Age,
                ActivityLevel=Profile.ActivityLevel.ToString(),
                FitnessGoal=Profile.fitnessGoal.ToString()
            };
            return Profile;
        }
    }
}
