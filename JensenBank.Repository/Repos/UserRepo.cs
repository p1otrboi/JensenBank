using Dapper;
using JensenBank.Core.Domain;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;

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
        var sql = $"INSERT INTO Users (CustomerId, Username, PW_Hash, PW_Salt, User_RoleId)" +
            $"VALUES ({user.CustomerId}, '{user.Username}', '{user.PW_Hash}', '{user.PW_Salt}', " +
            $"{user.User_RoleId}) " +
            $"SELECT Scope_identity()";

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sql);

            return id;
        }
    }

    public async Task<User> GetByUsername(string username)
    {
        var sql = $"SELECT UserId, CustomerId, Username, PW_Hash, PW_Salt, UR.Role_Type " +
            $"FROM Users U INNER JOIN User_Roles UR ON U.User_RoleId = UR.User_RoleId " +
            $"WHERE Username = '{username}'";

        using (var db = _context.CreateConnection())
        {
            var user = await db.QuerySingleOrDefaultAsync<User>(sql);

            return user;
        }
    }
}
