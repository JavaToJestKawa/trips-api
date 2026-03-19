using APBD_CW10.Data;
using APBD_CW10.DTOs;
using APBD_CW10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW10.Repositories;

public class TripsRepository : ITripsRepository
{
    private readonly ApbdCw10Context _context;

    public TripsRepository(ApbdCw10Context context)
    {
        _context = context;
    }
    
    public Task<Trip?> GetByIdAsync(int tripId, CancellationToken cancellationToken)
    {
        return _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == tripId, cancellationToken);
    }

    public async Task<TripResponse> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        // var trips = await _context.Trips.ToListAsync(cancellationToken);
        var query = _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom);
        
        var totalTrips = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);
        
        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var tripsDto = trips.Select(t => new TripDto
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(c => new CountryDto { Name = c.Name }).ToList(),
            Clients = t.ClientTrips.Select(ct => new ClientDto
            {
                FirstName=ct.IdClientNavigation.FirstName,
                LastName=ct.IdClientNavigation.LastName
            }).ToList()
        });

        return new TripResponse
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = totalPages,
            Trips = tripsDto.ToList()
        };
    }
}