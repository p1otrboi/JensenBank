namespace JensenBank.Core.Dto
{
    public class MoneyTransferDto
    {
        public int From_Account { get; set; }
        public int To_Account { get; set; }
        public decimal Amount { get; set; }
    }
}
