using Entites.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service=service;
        }


        [Authorize(Roles ="Admin")]
        [HttpPost]
     
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistration userForRegistrationDto)
        {
            var result= await _service.AuthenticationService.RegisterUser(userForRegistrationDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);    
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AuthenTicate([FromBody] userForAuthenticationDto user)
        {
            if(! await _service.AuthenticationService.ValidateUser(user)) return Unauthorized();
            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);
            return Ok(tokenDto);
        }
    }
}
