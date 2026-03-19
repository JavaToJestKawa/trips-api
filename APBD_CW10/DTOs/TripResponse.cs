namespace APBD_CW10.DTOs;

public class TripResponse
{
    public int PageNum{get;set;}
    public int PageSize{get;set;}
    public int AllPages{get;set;}
    public IEnumerable<TripDto> Trips { get; set; } = new List<TripDto>();
}

public class TripDto
{
   public string Name { get; set; } = null!;
   public string Description { get; set; } = null!;
   public DateTime DateFrom { get; set; }
   public DateTime DateTo { get; set; }
   public int MaxPeople { get; set; }
   public List<CountryDto> Countries { get; set; }
   public List<ClientDto> Clients { get; set; }
}

public class ClientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class CountryDto
{
    public string Name { get; set; } = null!;
}