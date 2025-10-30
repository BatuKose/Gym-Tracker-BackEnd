using Marvin.Cache.Headers;
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
    [ApiExplorerSettings(GroupName ="V1")]
    [ApiController]
    [Route("api/profile")]
    [Produces("application/json")]
    [HttpCacheExpiration(MaxAge =300,CacheLocation =CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate =true)]
    public class UserProfileController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserProfileController(IServiceManager manager)
        {
            _manager=manager;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task< IActionResult> GetUserProfileAsync([FromRoute(Name ="id")] int id,bool trackChanges)
        {
            var profile= await _manager.UserProfileService.GetUserProfileAsync(id, false);
            return Ok(profile);
        }
        [Authorize(Roles = "Admin")]
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS");
            Response.Headers.Add("Accept", "application/json, application/xml");
            return Ok();
        }
    }
}
