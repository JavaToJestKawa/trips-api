using APBD_CW10.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _clientsService.DeleteClientAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}