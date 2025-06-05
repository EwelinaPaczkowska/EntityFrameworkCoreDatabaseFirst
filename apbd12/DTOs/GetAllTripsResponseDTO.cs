namespace apbd12.DTOs;

public class GetAllTripsResponseDTO
{
    public int pageNum { get; set; }
    public int pageSize { get; set; }
    public int allPages { get; set; }
    public List<TripResponseDTO> Trips { get; set; }
}