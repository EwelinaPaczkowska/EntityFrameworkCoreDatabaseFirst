using apbd12.DAL;
using apbd12.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace apbd12.Services;

public class ClientService : IClientService
{
    private readonly TripsContext _context;
    
    public ClientService(TripsContext context)
    {
        _context = context;
    }
    
    public async Task DeleteClient(CancellationToken token, int id)
    {
        if(id < 1)
            throw new BadRequestException("Id must be greater than 0");
        
        var client = await _context.Clients
            .Where(c => c.IdClient == id)
            .FirstOrDefaultAsync(token);
        if (client == null)
            throw new NotFoundException("client not found");
        
        var clientTrips = await _context.ClientTrips.Where(t=>t.IdClient == id).ToListAsync(token);
        if(clientTrips.Any())
            throw new ConflictException("Client has trips assigned, it cant be deleted.");
        
        _context.Clients.Remove(client);
        
        await _context.SaveChangesAsync(token);
    }
}