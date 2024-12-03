using Microsoft.AspNetCore.Mvc;
using SportTasksCalendar.Application.Models;

namespace SportTasksCalendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseCategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetExerciseCategories()
        {
            var categories = ExerciseCategoryInfo.ExerciseCategories;
            return Ok(categories);
        }
    }
}