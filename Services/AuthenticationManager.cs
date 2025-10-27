using AutoMapper;
using Entites.DataTransferObject;
using Entites.Exceptions;
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
using System.Security.Cryptography;
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

        
        public async Task<TokenDto> CreateToken(bool populateExp)
        {

            var siginCredentials = GetSinginCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(siginCredentials, claims);
            var refreshToken = GenerateRefresToken();
            if(populateExp)
            {
                _user.RefreshTokenExpiryTime=DateTime.Now.AddDays(7);
            }
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private  string GenerateRefresToken()
        {
            var randomNumber= new byte[32];
            using(var rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        
        private ClaimsPrincipal GetPrincipalFromExpriedToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"]; 

            var tokenValidationParametres = new TokenValidationParameters
            {
                ValidateIssuer = true,           
                ValidateAudience = true,         
                ValidateLifetime = true,        
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],      
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal=tokenHandler.ValidateToken(token,tokenValidationParametres, out securityToken);
            var jwtSecurityToken=securityToken as JwtSecurityToken;

            if(jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenArgumentException("Invalid Token");
            }
            return principal;
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

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal=GetPrincipalFromExpriedToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if(user is null || user.RefreshToken!=tokenDto.RefreshToken || user.RefreshTokenExpiryTime<=DateTime.Now)
            {
                throw new RefreshTokenBadReqruestExceptin();
            }
            _user=user;
            return await CreateToken(populateExp: false);
        }
    }
}
