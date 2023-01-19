namespace Models.Domain;

public class CustomerForCreationDto
{
    public required string Gender { get; set; }

    public required string Givenname { get; set; }

    public required string Surname { get; set; }

    public required string Streetaddress { get; set; }

    public required string City { get; set; }

    public required string Zipcode { get; set; }

    public required string Country { get; set; }

    public required string CountryCode { get; set; }

    public DateOnly Birthday { get; set; }

    public string? Telephonecountrycode { get; set; }

    public string? Telephonenumber { get; set; }

    public string? Emailaddress { get; set; }
}
