using Dapper;
using JensenBank.Repository.Context;
using JensenBank.Repository.Interfaces;
using Microsoft.Identity.Client;
using Models.Domain;

namespace JensenBank.Repository.Repos;

public class CustomerRepo : ICustomerRepo
{
    private readonly DapperContext _context;
    public CustomerRepo(DapperContext context)
    {
        _context = context;
    }
    public async Task<int> AddAsync(CustomerForCreationDto customer)
    {
        var sql = $"INSERT INTO Customers (Gender, Givenname, Surname, Streetaddress, City, Zipcode, " +
            $"Country, CountryCode, Birthday, Telephonecountrycode, Telephonenumber, Emailaddress) " +
            $"VALUES ('{customer.Gender}', '{customer.Givenname}', '{customer.Surname}', '{customer.Streetaddress}', " +
            $"'{customer.City}', '{customer.Zipcode}', '{customer.Country}', '{customer.CountryCode}', '{customer.Birthday}', " +
            $"'{customer.Telephonecountrycode}', '{customer.Telephonenumber}', '{customer.Emailaddress}') " +
            $"SELECT Scope_identity()";

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sql);

            return id;
        }
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM Customers WHERE CustomerId = {id}";

        using (var db = _context.CreateConnection())
        {
            var user = await db.QuerySingleOrDefaultAsync<Customer>(sql);

            return user;
        }
    }
}
