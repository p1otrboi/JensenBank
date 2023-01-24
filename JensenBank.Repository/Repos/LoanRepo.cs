using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Repository.Context;
using JensenBank.Repository.Interfaces;
using System.Data;
using Models.Domain;

namespace JensenBank.Repository.Repos
{
    public class LoanRepo : ILoanRepo
    {
        private readonly DapperContext _context;
        public LoanRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM Loans WHERE LoanId = {id}";

            using (var db = _context.CreateConnection())
            {
                var loan = await db.QuerySingleOrDefaultAsync<Loan>(sql);

                return loan;
            }
        }

        public async Task<int> AddAsync(LoanForCreationDto loan)
        {
            decimal payments = loan.Amount/loan.Duration_Months;

            var sp = "CreateLoan";

            var param = new DynamicParameters();

            param.Add("@AccountId", loan.AccountId);
            param.Add("@Amount", loan.Amount);
            param.Add("@Duration", loan.Duration_Months);
            param.Add("@Payments", payments);


            using (var db = _context.CreateConnection())
            {
                var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

                return id;
            }
        }
    }
}
