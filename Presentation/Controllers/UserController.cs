using Entites.DataTransferObject.User;
using Entites.Models;
using Entites.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUser = Entites.Models.User;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")]
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")] // ilerde kaldırılcak
    
    public class UserController :ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
       
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task< IActionResult> CreateUser([FromBody] MyUser user)
        {       
            
                await _serviceManager.UserService.CreateUser(user);
                return StatusCode(201, user);
        }
        [HttpGet]
        [Authorize]
        public async Task< IActionResult> GettAllUser([FromQuery] UserParameters userParameters)
        {
           
                var User  = await  _serviceManager.UserService.GetAllUsersAsync(userParameters,false);
                return Ok(User);
            
        }
        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetUserById([FromRoute(Name ="id")] int id)
        {       
                
                var user = await _serviceManager.UserService.GetUserByIdAsync(id, false);
                return Ok(user);    
        }
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(LogFilterAttribute))]
        public IActionResult DeleteUser([FromRoute(Name ="id")] int id)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
            _serviceManager.UserService.DeleteUser(id,false);
                return NoContent();    
        }
        [HttpPut("{id:int}")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task< IActionResult> UpdateUser([FromRoute(Name ="id")] int id, [FromBody] User user)
        {
        
            var entity = await _serviceManager.UserService.GetUserByIdAsync(id, true);
              await  _serviceManager.UserService.UpdateUser(id,user,true);
                return Ok("Kullanıcı Güncellendi");  
        }
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateUser([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<MyUser> user)
        {
            
            var entiy = await _serviceManager.UserService.GetUserByIdAsync(id, true);    
            user.ApplyTo(entiy);
          await  _serviceManager.UserService.UpdateUser(id, entiy, true);
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetUserOptions()
        {
            
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            Response.Headers.Add("Accept","application/json, application/xml");
            return Ok();
        }
    }
}
