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

    public required DateOnly Birthday { get; set; }

    public required string Telephonecountrycode { get; set; }

    public required string Telephonenumber { get; set; }

    public required string Emailaddress { get; set; }
    public required string Desired_Username { get; set; }
    public required string Desired_Password { get; set; }
}
