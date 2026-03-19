namespace APBD_CW10.Services;

public interface IClientsService
{
    Task<bool> DeleteClientAsync(int clientId, CancellationToken cancellationToken);
}