using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using Models.Domain;
using System.Data;

namespace JensenBank.Infrastructure.Repos
{
    public class LoanRepo : ILoanRepo
    {
        private readonly DapperContext _context;
        public LoanRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetByIdAsync(int loanId)
        {
            var sp = "LoanGetById";
            var param = new DynamicParameters();
            param.Add("@LoanId", loanId);

            using (var db = _context.CreateConnection())
            {
                var loan = await db.QuerySingleOrDefaultAsync<Loan>(sp, param, commandType: CommandType.StoredProcedure);

                return loan;
            }
        }

        public async Task<int> AddAsync(LoanForCreationDto loan)
        {
            decimal payments = loan.Amount / loan.Duration_Months;

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
