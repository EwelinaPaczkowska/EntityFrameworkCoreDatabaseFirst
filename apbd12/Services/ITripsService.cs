using apbd12.DTOs;

namespace apbd12.Services;

public interface ITripsService
{
    public Task<object> GetAllTrips(int page, int pageSize, CancellationToken token);
    public Task AssignClientToTrip(int id, AssignClientToTripRequestDTO dto, CancellationToken token);
}