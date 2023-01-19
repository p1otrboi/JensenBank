namespace Models.Domain;

public partial class Account
{
    public int AccountId { get; set; }

    public required string Frequency { get; set; }

    public DateOnly Created { get; set; }

    public decimal Balance { get; set; }

    public int? AccountTypesId { get; set; }

    public virtual AccountType? AccountTypes { get; set; }

    public virtual ICollection<Disposition> Dispositions { get; } = new List<Disposition>();

    public virtual ICollection<Loan> Loans { get; } = new List<Loan>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
