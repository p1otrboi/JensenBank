namespace JensenBank.Core.Dto
{
    public class AccountSummaryDto
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string? Account_Type { get; set; }
    }
}
