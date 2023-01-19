using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Repository.Context;
using JensenBank.Repository.Interfaces;

namespace JensenBank.Repository.Repos;

public class AccountRepo : IAccountRepo
{
    private readonly DapperContext _context;

    public AccountRepo(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(AccountForCreationDto account)
    {
        var sql = $"INSERT INTO Accounts (Frequency, Created, Balance, AccountTypesId) " +
            $"VALUES ('{account.Frequency}', GETDATE(), 0, {account.AccountTypeId}) " +
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
}
