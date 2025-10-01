using AutoMapper;
using Entites.DataTransferObject.UserProfile;
using Entites.Exceptions;
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

       
        public async Task<UserProfileDto> GetUserProfileAsync(int id, bool trackChanges)
        {
            var profile = await _manager.userProfileRepository.GetUserProgileAsync(id, trackChanges);

            if (profile is null) throw new UserNotFoundExeption();


            var dto = new UserProfileDto()
            {
                UserName = profile.User.Username,
                Erkekmi = profile.User.erkekMi,
                Weight = profile.Weight,
                Height = profile.Height,
                Age = profile.Age,
                ActivityLevel = profile.ActivityLevel.ToString(),
                FitnessGoal = profile.fitnessGoal.ToString()
            };

            return dto;
        }

        
    }
}
