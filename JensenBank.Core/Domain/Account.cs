﻿namespace Models.Domain;

public partial class Account
{
    public int AccountId { get; set; }

    public required string Frequency { get; set; }

    public DateTime Created { get; set; }

    public decimal Balance { get; set; }

    public int? AccountTypesId { get; set; }

    public virtual AccountType? AccountTypes { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
