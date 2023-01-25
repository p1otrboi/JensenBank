namespace JensenBank.Core.Dto
{
    public class TransactionForCreationDto
    {
        public required int AccountId { get; set; }
        public required string Type { get; set; }
        public required string Operation { get; set; }
        public required decimal Amount { get; set; }
        public required decimal Balance { get; set; }
        public string? Account { get; set; }
    }
}
