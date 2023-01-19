namespace JensenBank.Core.Dto
{
    public class AccountForCreationDto
    {
        public required string Frequency { get; set; }
        public int? AccountTypeId { get; set; }
    }
}
