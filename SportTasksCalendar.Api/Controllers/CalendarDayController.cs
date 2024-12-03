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
    public class CalendarDayController(ICalendarDayService calendarDayService) : ControllerBase
    {
        private readonly ICalendarDayService _calendarDayService = calendarDayService;

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCalendarDayById(Guid id)
        {
            var calendarDay = await _calendarDayService.GetCalendarDayByIdAsync(id);
            if (calendarDay == null)
            {
                return NotFound();
            }

            return Ok(calendarDay);
        }

        [HttpGet("by-calendar/{calendarId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCalendarDaysByCalendarId(Guid calendarId)
        {
            var calendarDays = await _calendarDayService.GetCalendarDaysByCalendarId(calendarId);
            return Ok(calendarDays);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCalendarDay(CalendarDayRequest calendarDayRequest)
        {
            try
            {
                await _calendarDayService.AddCalendarDayAsync(calendarDayRequest.ToEntity(out var newCalendarDayId));
                return CreatedAtAction(nameof(GetCalendarDayById), new { id = newCalendarDayId }, calendarDayRequest);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCalendarDay(Guid id, CalendarDay calendarDay)
        {
            if (id != calendarDay.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _calendarDayService.UpdateCalendarDayAsync(calendarDay);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCalendarDay(Guid id)
        {
            await _calendarDayService.DeleteCalendarDayAsync(id);
            return NoContent();
        }
    }
}