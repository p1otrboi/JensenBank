﻿using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Repository.Context;
using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Repository.Repos;

public class AccountRepo : IAccountRepo
{
    private readonly DapperContext _context;

    public AccountRepo(DapperContext context)
    {
        _context = context;
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
}