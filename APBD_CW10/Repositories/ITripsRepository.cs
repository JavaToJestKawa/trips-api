using APBD_CW10.DTOs;
using APBD_CW10.Models;

namespace APBD_CW10.Repositories;

public interface ITripsRepository
{
    Task<Trip?> GetByIdAsync(int tripId, CancellationToken cancellationToken);
    Task<TripResponse> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);
}