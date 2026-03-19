using APBD_CW10.DTOs;
using APBD_CW10.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;

    public TripsController(ITripsService tripsService)
    {
        this._tripsService = tripsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTripsAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var trips = await _tripsService.GetTripsAsync(page, pageSize, cancellationToken);
        return Ok(trips);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip,
        [FromBody] AssignClientToTripRequest dto,
        CancellationToken cancellationToken)
    {
        try
        {
            await _tripsService.AssignClientToTripAsync(idTrip, dto, cancellationToken);
            return Ok("Client assigned successfully.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}