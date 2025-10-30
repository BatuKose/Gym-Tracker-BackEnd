using Entites.DataTransferObject.Exercise;
using Entites.Models;
using Entites.RequestFeatures;
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
    [ApiExplorerSettings(GroupName = "V1")]
    [ApiController]
    [Route("api/exercise")]
    [Produces("application/json")] // ilerde kaldırılcak
    public class ExerciseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ExerciseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [Authorize]

        [HttpPost("{id:int}")]
        public async Task<IActionResult> CreateOneExercise([FromRoute(Name = "id")] int id, [FromBody] ExerciseForCreationDto dto)
        {
            await _serviceManager.ExerciseService.CreateExercise(dto, id);
            return StatusCode(201);
        }
        [Authorize]
        [HttpHead] // kontrol
        [HttpGet("exercise/users/{id:int}")]
        [Produces("application/json")]
        public IActionResult GetUserWithExercises([FromRoute(Name = "id")] int id, [FromQuery] UserWithExerciseParameters userWithExerciseParameters)
        {
            var User = _serviceManager.UserService.GetUserWithExercises(userWithExerciseParameters, id);
            return Ok(User);
        }
        [Authorize (Roles = "Admin,Antreneör")]
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [HttpCacheExpiration(MaxAge =60,CacheLocation =CacheLocation.Public)]
        [HttpCacheValidation(MustRevalidate = true)]
        public IActionResult GetExerciseById([FromRoute(Name = "id")] int id)
        {
            var exerciseDto = _serviceManager.ExerciseService.GetExerciseById(id, false);
            return Ok(exerciseDto);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult UpdateExercise([FromRoute(Name = "id")] int id, ExerciseForUpdateDto exerciseDto)
        {

            _serviceManager.ExerciseService.UpdateExercise(exerciseDto, id);
            return NoContent();

        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteExercise([FromRoute(Name ="id")] int id)
        {
            _serviceManager.ExerciseService.DeleteExerciseById(id);
            return NoContent();
        }
        [Authorize(Roles ="Admin")]
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Accept","application/json, application/xml");
            return Ok();
        }
    }
}
