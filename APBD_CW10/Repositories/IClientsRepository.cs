namespace APBD_CW10.Repositories;

public interface IClientsRepository
{
    Task<bool> DeleteClientAsync(int clientId, CancellationToken cancellationToken);
}