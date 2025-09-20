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
    [Route("api/profile")]
    [Produces("application/json")] // ilerde kaldırılcak
    public class UserProfileController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserProfileController(IServiceManager manager)
        {
            _manager=manager;
        }

        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetUserProfileAsync([FromRoute(Name ="id")] int id,bool trackChanges)
        {
            var profile= await _manager.UserProfileService.GetUserProfileAsync(id, false);
            return Ok(profile);
        }
    }
}
