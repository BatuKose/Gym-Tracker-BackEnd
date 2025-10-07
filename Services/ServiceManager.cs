using AutoMapper;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly Lazy<IAuthenticationService> _authenticationService;

       

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService loggerService,IMapper mapper,IConfiguration configuration,UserManager<UserBase> userManager)
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager,loggerService,mapper));
            _exerciseService = new Lazy<IExerciseService>(()=> new ExerciseManager(repositoryManager,loggerService,mapper));
            _profileService= new Lazy<IUserProfileService>(()=> new UserProfileManager(repositoryManager,loggerService,mapper));
            _authenticationService= new Lazy<IAuthenticationService>(()=>new AuthenticationManager(loggerService,mapper,userManager,configuration));
        }
        public IUserService UserService => _userService.Value;
        public IExerciseService ExerciseService => _exerciseService.Value;
        public  IUserProfileService UserProfileService => _profileService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
