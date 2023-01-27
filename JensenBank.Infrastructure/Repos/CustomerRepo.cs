using Dapper;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using Models.Domain;
using System.Data;

namespace JensenBank.Infrastructure.Repos;

public class CustomerRepo : ICustomerRepo
{
    private readonly DapperContext _context;
    public CustomerRepo(DapperContext context)
    {
        _context = context;
    }
    public async Task<int> AddAsync(CustomerForCreationDto customer)
    {
        var sp = "CustomerAdd";
        var param = new DynamicParameters();
        param.Add("@Gender", customer.Gender);
        param.Add("@Givenname", customer.Givenname);
        param.Add("@Surname", customer.Surname);
        param.Add("@Streetaddress", customer.Streetaddress);
        param.Add("@City", customer.City);
        param.Add("@Zipcode", customer.Zipcode);
        param.Add("@Country", customer.Country);
        param.Add("@CountryCode", customer.CountryCode);
        param.Add("@Birthday", customer.Birthday);
        param.Add("@Telephonecountrycode", customer.Telephonecountrycode);
        param.Add("@Telephonenumber", customer.Telephonenumber);
        param.Add("@Emailaddress", customer.Emailaddress);

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

            return id;
        }
    }

    public async Task<Customer> GetByIdAsync(int customerId)
    {
        var sp = "CustomerGetById";
        var param = new DynamicParameters();
        param.Add("@CustomerId", customerId);

        using (var db = _context.CreateConnection())
        {
            var user = await db.QuerySingleOrDefaultAsync<Customer>(sp, param, commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}
