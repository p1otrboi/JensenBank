using Dapper;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using System.Data;

namespace JensenBank.Infrastructure.Repos;

public class DispositionRepo : IDispositionRepo
{
    private readonly DapperContext _context;
    public DispositionRepo(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(int customerId, int accountId, string type)
    {
        var sp = "DispositionAdd";
        var param = new DynamicParameters();
        param.Add("@CustomerId", customerId);
        param.Add("@AccountId", accountId);
        param.Add("@Type", type);

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

            return id;
        }
    }
}
