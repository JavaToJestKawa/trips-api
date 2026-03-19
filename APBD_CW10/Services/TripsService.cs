using APBD_CW10.Data;
using APBD_CW10.DTOs;
using APBD_CW10.Models;
using APBD_CW10.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW10.Services;

public class TripsService : ITripsService
{
    private readonly ITripsRepository _tripsRepository;
    private readonly ApbdCw10Context _context;

    public TripsService(
        ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }
    
    public async Task<TripResponse> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _tripsRepository.GetTripsAsync(page, pageSize, cancellationToken);
        // return new TripResponse();
    }
    
    // Pominięty podział na repozytoria
    public async Task AssignClientToTripAsync(int tripId, AssignClientToTripRequest dto, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .Include(t => t.ClientTrips)
            .FirstOrDefaultAsync(t => t.IdTrip == tripId, cancellationToken);

        if (trip is null)
            throw new Exception("Trip does not exist.");

        if (trip.DateFrom < DateTime.Now)
            throw new Exception("Cannot assign to a past trip.");

        var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel, cancellationToken);
        if (existingClient is not null)
        {
            var alreadyAssigned = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == tripId, cancellationToken);
            if (alreadyAssigned)
                throw new Exception("Client already assigned to this trip.");
        }

        // Create new client if not found
        var client = existingClient ?? new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        if (existingClient is null)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync(cancellationToken);
        }

        _context.ClientTrips.Add(new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = tripId,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        });

        await _context.SaveChangesAsync(cancellationToken);

    }

    public Task<IEnumerable<Trip>> GetTripsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}