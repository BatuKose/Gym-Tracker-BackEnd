using AutoMapper;
using Entites.DataTransferObject;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserBase> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<UserBase> userManager, IConfiguration configuration)
        {
            _logger=logger;
            _mapper=mapper;
            _userManager=userManager;
            _configuration=configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistration userForRegistrationDto)
        {
             var user=_mapper.Map<UserBase>(userForRegistrationDto);
            var result= await _userManager.CreateAsync(user,userForRegistrationDto.passWord);
            if (result.Succeeded) await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            return result;

        }
    }
}
