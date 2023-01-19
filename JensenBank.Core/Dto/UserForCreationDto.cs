namespace JensenBank.Core.Dto;

public class UserForCreationDto
{
    public required int CustomerId { get; set; }
    public required int User_RoleId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
