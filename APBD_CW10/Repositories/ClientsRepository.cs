using APBD_CW10.Data;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW10.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly ApbdCw10Context _context;

    public ClientsRepository(ApbdCw10Context context)
    {
        _context = context;
    }
    
    public async Task<bool> DeleteClientAsync(int clientId, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .FindAsync(clientId, cancellationToken);
        
        if (client == null)
        {
            throw new Exception($"Client with ID {clientId} not found.");
        }
        
        var hasTrips = await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == clientId, cancellationToken);

        if (hasTrips)
        {
            throw new Exception($"Client with ID {clientId} cannot be deleted – assigned to at least one trip.");
        }
        
        var remove = _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}