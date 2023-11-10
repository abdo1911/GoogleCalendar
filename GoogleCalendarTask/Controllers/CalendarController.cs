using Google_Calendar_Task.Models;
using GoogleCalendarTask.Helpers;
using GoogleCalendarTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleCalendarTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Calendar : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateGoogleCalender([FromBody] GoogleCalander request)
        {
            return Ok(await GoogleCalanderHelper.CreateGoogleCalander(request));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllGoogleCalenderEvents([FromBody] PaginationRequest request)
        {
            try
            {
                return Ok(await GoogleCalanderHelper.GetAll(request));

            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("get/{eventId}")]
        public async Task<IActionResult> GetGoogleCalenderEvent(string eventId)
        {
            try
            {
                var eventDetails = await GoogleCalanderHelper.GetGoogleCalanderEvent(eventId);
                return Ok(eventDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{eventId}")]
        public async Task<IActionResult> DeleteGoogleCalenderEvent(string eventId)
        {
            try
            {
                await GoogleCalanderHelper.DeleteGoogleCalanderEvent(eventId);
                return Ok($"Event with ID {eventId} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }


}
