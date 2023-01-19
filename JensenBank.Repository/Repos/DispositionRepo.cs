﻿using Dapper;
using JensenBank.Repository.Context;

namespace JensenBank.Repository.Repos;

public class DispositionRepo
{
    private readonly DapperContext _context;
    public DispositionRepo(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(int customerId, int accountId, string type)
    {
        var sql = $"INSERT INTO Dispositions (CustomerId, AccountId, Type) " +
            $"VALUES ({customerId}, {accountId}, '{type}') " +
            $"SELECT Scope_identity()";

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sql);

            return id;
        }
    }
}
