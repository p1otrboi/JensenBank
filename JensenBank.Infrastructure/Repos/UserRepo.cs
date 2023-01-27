using Dapper;
using JensenBank.Core.Domain;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using System.Data;

namespace JensenBank.Infrastructure.Repos;

public class UserRepo : IUserRepo
{
    private readonly DapperContext _context;
    public UserRepo(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(DbUserForCreationDto user)
    {
        var sp = "UserAdd";
        var param = new DynamicParameters();
        param.Add("@CustomerId", user.CustomerId);
        param.Add("@Username", user.Username);
        param.Add("@PW_Hash", user.PW_Hash);
        param.Add("@PW_Salt", user.PW_Salt);
        param.Add("@User_RoleId", user.User_RoleId);

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

            return id;
        }
    }

    public async Task<User> GetByUsername(string username)
    {
        var sp = "UserGetByUsername";
        var param = new DynamicParameters();
        param.Add("@Username", username);

        using (var db = _context.CreateConnection())
        {
            var user = await db.QuerySingleOrDefaultAsync<User>(sp, param, commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
