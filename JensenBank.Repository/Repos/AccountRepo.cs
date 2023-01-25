using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using Models.Domain;

namespace JensenBank.Infrastructure.Repos;

public class AccountRepo : IAccountRepo
{
    private readonly DapperContext _context;

    public AccountRepo(DapperContext context)
    {
        _context = context;
    }
    public async Task<decimal> GetBalanceAsync(int accountId)
    {
        var sql = $"SELECT Balance FROM Accounts WHERE AccountId = {accountId}";

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QueryFirstOrDefaultAsync<decimal>(sql);

            return balance;
        }
    }
    public async Task<Account> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM Accounts WHERE AccountId = {id}";

        using (var db = _context.CreateConnection())
        {
            var account = await db.QuerySingleOrDefaultAsync<Account>(sql);

            return account;
        }
    }
    public async Task<int> AddAsync(AccountForCreationDto account)
    {
        var sql = $"INSERT INTO Accounts (Frequency, Created, Balance, AccountTypesId) " +
            $"VALUES ('{account.Frequency}', CONVERT(VARCHAR(10), getdate(), 111), 0, {account.AccountTypeId}) " +
            $"SELECT Scope_identity()";

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sql);

            return id;
        }
    }

    public async Task<int> SetAccountType(int accountId, int accountTypeId)
    {
        var sql = $"UPDATE Accounts SET AccountTypesId = {accountTypeId} " +
            $"WHERE AccountId = {accountId} " +
            $"SELECT Scope_identity()";

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sql);

            return id;
        }
    }

    public async Task<decimal> AddAmountToAccountBalanceAsync(int accountId, decimal amount)
    {
        var sql = $"UPDATE Accounts SET Balance = Balance + {amount} " +
            $"WHERE AccountId = {accountId} " +
            $"SELECT Balance FROM Accounts WHERE AccountId = {accountId}";

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QuerySingleAsync<decimal>(sql);

            return balance;
        }
    }

    public async Task<decimal> SubAmountFromAccountBalanceAsync(int accountId, decimal amount)
    {
        var sql = $"UPDATE Accounts SET Balance = Balance - {amount} " +
            $"WHERE AccountId = {accountId} " +
            $"SELECT Balance FROM Accounts WHERE AccountId = {accountId}";

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QuerySingleAsync<decimal>(sql);

            return balance;
        }
    }

    public async Task<List<AccountSummaryDto>> GetAccountSummary(int customerId)
    {
        var sql = $"SELECT A.AccountId, A.Balance, Acct.TypeName as 'Account_Type' FROM Accounts A " +
            $"INNER JOIN Dispositions D ON D.AccountId = A.AccountId " +
            $"INNER JOIN AccountTypes AccT ON AccT.AccountTypeId = A.AccountTypesId " +
            $"WHERE D.CustomerId = {customerId}";

        using (var db = _context.CreateConnection())
        {
            var result = await db.QueryAsync<AccountSummaryDto>(sql);

            return result.ToList();
        }
    }

    public async Task<AccountTransactionsDto> GetAccountWithTransactions(int accountId)
    {
        var sql = $"SELECT * FROM Accounts " +
            $"INNER JOIN Transactions ON Transactions.AccountId = Accounts.AccountId " +
            $"WHERE Accounts.AccountId = {accountId} " +
            $"ORDER BY Transactions.TransactionId DESC";

        using (var db = _context.CreateConnection())
        {
            var lookup = new Dictionary<int, AccountTransactionsDto>();

            var result = await db.QueryAsync<AccountTransactionsDto, Transaction, AccountTransactionsDto>(sql, (account, transaction) =>
            {
                if (!lookup.TryGetValue(account.AccountId, out AccountTransactionsDto a))
                    lookup.Add(account.AccountId, a = account);

                if (account.Transactions is null)
                    a.Transactions = new List<Transaction>();

                a.Transactions.Add(transaction);

                return a;
            }, splitOn: "TransactionId");

            return result.First();
        }
    }
}