﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SportTasksCalendar.Application.Models;
using SportTasksCalendar.Application.Services;
using SportTasksCalendar.Contracts;
using SportTasksCalendar.Contracts.DTOs;

namespace SportTasksCalendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController(ICalendarService calendarService) : ControllerBase
    {
        private readonly ICalendarService _calendarService = calendarService;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCalendarById(Guid id)
        {
            var calendar = await _calendarService.GetCalendarByIdAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCalendars()
        {
            var calendars = await _calendarService.GetAllCalendarsAsync();
            return Ok(calendars);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCalendar(CalendarRequest calendarRequest)
        {
            try
            {
                await _calendarService.AddCalendarAsync(calendarRequest.ToEntity(out var newCalendarId));
                return CreatedAtAction(nameof(GetCalendarById), new { id = newCalendarId }, calendarRequest);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCalendar(Guid id, Calendar calendar)
        {
            if (id != calendar.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _calendarService.UpdateCalendarAsync(calendar);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCalendar(Guid id)
        {
            await _calendarService.DeleteCalendarAsync(id);
            return NoContent();
        }
    }
}