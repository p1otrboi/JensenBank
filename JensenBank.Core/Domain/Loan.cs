namespace Models.Domain;

public partial class Loan
{
    public int LoanId { get; set; }

    public int AccountId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public int Duration { get; set; }

    public decimal Payments { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
