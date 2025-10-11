using AutoMapper;
using Entites.DataTransferObject;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private UserBase? _user;
        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<UserBase> userManager, IConfiguration configuration)
        {
            _logger=logger;
            _mapper=mapper;
            _userManager=userManager;
            _configuration=configuration;
        }

        public async Task<string> CreateToken()
        {

            var siginCredentials = GetSinginCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(siginCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials siginCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
                (
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials:siginCredentials
                    
                );
            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,_user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }
            return claims;
        }

        private SigningCredentials GetSinginCredentials()
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(JwtSettings["secretKey"]);
            var secret=new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistration userForRegistrationDto)
        {
             var user=_mapper.Map<UserBase>(userForRegistrationDto);
            var result= await _userManager.CreateAsync(user,userForRegistrationDto.passWord);
            if (result.Succeeded) await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            return result;

        }
         
        public async Task<bool> ValidateUser(userForAuthenticationDto userForAuthenticationDto)
        {
            _user= await _userManager.FindByNameAsync(userForAuthenticationDto.userName);
            var result = (_user !=null && await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.PassWord));
            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)}: Doğrulama başarısız oldu. Kullanıcı adı veya şifre yanlış.");
            }

            return result;

        }
    }
}
