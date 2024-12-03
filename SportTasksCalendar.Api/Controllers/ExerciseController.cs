using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Services;
using SportTasksCalendar.Contracts;
using SportTasksCalendar.Contracts.DTOs;

namespace SportTasksCalendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController(IExerciseService exerciseService) : ControllerBase
    {
        private readonly IExerciseService _exerciseService = exerciseService;

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExerciseById(Guid id)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpGet("by-calendar-day/{calendarDayId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllExercisesByCalendarDayId(Guid calendarDayId)
        {
            var exercises = await _exerciseService.GetAllExercisesByCalendarDayIdAsync(calendarDayId);
            return Ok(exercises);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddExercise(ExerciseRequest exerciseRequest)
        {
            try
            {
                await _exerciseService.AddExerciseAsync(exerciseRequest.ToEntity(out var newExerciseId));
                return CreatedAtAction(nameof(GetExerciseById), new { id = newExerciseId }, exerciseRequest);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateExercise(Guid id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _exerciseService.UpdateExerciseAsync(exercise);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}