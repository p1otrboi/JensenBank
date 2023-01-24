using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Repository.Context;

namespace JensenBank.Repository.Repos
{
    public class LoanRepo
    {
        private readonly DapperContext _context;
        public LoanRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(LoanForCreationDto loan)
        {
            decimal payments = loan.Amount/loan.Duration_Months;

            var sql = $"INSERT INTO Loans (AccountId, Date, Amount, Duration, Payments, Status) " +
                $"VALUES ({loan.AccountId}, GETDATE(), {loan.Amount}, {loan.Duration_Months}, {payments}, 'Running') " +
                $"SELECT SCOPE_IDENTITY()";

            using (var db = _context.CreateConnection())
            {
                var id = await db.ExecuteScalarAsync<int>(sql);

                return id;
            }
        }
    }
}
