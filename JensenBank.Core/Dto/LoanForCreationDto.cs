namespace JensenBank.Core.Dto
{
    public class LoanForCreationDto
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public required int Duration_Months { get; set; }
    }
}
