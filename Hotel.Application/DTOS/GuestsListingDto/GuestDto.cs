namespace Hotel.Application.DTOS.GuestsListingDto;

public class GuestDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public string? City { get; set; } = default!;
    public string? Street { get; set; } = default!;
    public string? PostalCode { get; set; } = default!;
}