using ExerciseService.Logic;
using ExerciseService.Models;
using ExerciseService.Transformers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ExerciseService.Controllers
{
    [ApiController]
    [Route("/exercises")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ExerciseController : Controller
    {
        private readonly ILogger<ExerciseController> logger;
        private ExerciseFacade exerciseFacade;

        public ExerciseController(ILogger<ExerciseController> logger, ExerciseFacade exerciseFacade)
        {
            this.logger = logger;
            this.exerciseFacade = exerciseFacade;
        }

        [HttpGet]
        [Route("/exercises/ping")]
        public IActionResult Ping()
        {
            return Ok("Hello world!");
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetPublicExercises([FromQuery] string q, [FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] Guid userId)
        {
            logger.LogInformation("The retrieval of a list of exercises is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToExercisePageReponse(
                        exerciseFacade.GetExercises(q, pageNumber, pageSize, userId)));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving list of exercises", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("/exercises/liked/users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetLikedExercises([FromRoute] Guid id, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            logger.LogInformation("The retrieval of a list of exercises liked by a specific user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToExercisePageReponse(
                        exerciseFacade.GetExercisesLikedByUser(id, pageNumber, pageSize)));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving list of exercises liked by a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("/exercises/users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetExercisesByUser([FromRoute] Guid id, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            logger.LogInformation("The retrieval of a list of exercises made by a specific user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToExercisePageReponse(
                        exerciseFacade.GetExercisesByUser(id, pageNumber, pageSize)));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving list of exercises belonging to a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("/exercises/{id}/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult LikeExercise([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            logger.LogInformation("Like exercise");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                exerciseFacade.LikeExercise(
                    new LikeDBO() { ExerciseId = id, UserId = userId });
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError("error occured when liking", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("/exercises/{id}/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UnlikeExercise([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            logger.LogInformation("Unlike exercise");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                exerciseFacade.UnlikeExercise(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError("error occured when unliking", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("/exercises/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetExercise([FromRoute] Guid id)
        {
            logger.LogInformation("The retrieval of an exercise is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToJsonModel(
                        exerciseFacade.GetExercise(id)));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving an exercise", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateExercise([FromBody] ExerciseJsonModel exercise)
        {
            logger.LogInformation("The creation of an exercise is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToJsonModel(
                        exerciseFacade.CreateExercise(
                            ExerciseTransformer.ToDataModel(exercise))));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when creating an exercise", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("/exercises/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteExercise([FromRoute] Guid id)
        {
            logger.LogInformation("The deletion of an exercise is requested");

            try
            {
                exerciseFacade.DeleteExercise(id);
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError("error occured when deleting an exercise", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("/exercises/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ExerciseJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult updateExercise([FromBody] ExerciseJsonModel exercise)
        {
            logger.LogInformation("The update of an exercise is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    ExerciseTransformer.ToJsonModel(
                        exerciseFacade.UpdateExercise(
                            ExerciseTransformer.ToDataModel(exercise))));

            }
            catch (Exception e)
            {
                logger.LogError("error occured when updating an exercise", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
