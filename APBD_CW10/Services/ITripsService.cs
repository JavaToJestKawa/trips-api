using APBD_CW10.DTOs;
using APBD_CW10.Models;

namespace APBD_CW10.Services;

public interface ITripsService
{
    Task<TripResponse> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task AssignClientToTripAsync(int tripId, AssignClientToTripRequest dto, CancellationToken cancellationToken);
}