using Models.Domain;

namespace JensenBank.Core.Dto
{
    public class AccountTransactionsDto
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
