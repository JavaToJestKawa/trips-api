using APBD_CW10.Repositories;

namespace APBD_CW10.Services;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }
    
    public async Task<bool> DeleteClientAsync(int clientId, CancellationToken cancellationToken)
    {
        return await _clientsRepository.DeleteClientAsync(clientId, cancellationToken);
    }
}