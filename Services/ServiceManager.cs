using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IExerciseService> _exerciseService;
        private readonly Lazy<IUserProfileService> _profileService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService loggerService,IMapper mapper )
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager,loggerService,mapper));
            _exerciseService = new Lazy<IExerciseService>(()=> new ExerciseManager(repositoryManager,loggerService,mapper));
            _profileService= new Lazy<IUserProfileService>(()=> new UserProfileManager(repositoryManager,loggerService,mapper));
        }
        public IUserService UserService => _userService.Value;
        public IExerciseService ExerciseService => _exerciseService.Value;
        public  IUserProfileService ProfileService => _profileService.Value;
    }
}
