using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Repository.Context;
using JensenBank.Repository.Interfaces;
using System.Data;

namespace JensenBank.Repository.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly DapperContext _context;

        public TransactionRepo(DapperContext dapperContext)
        {
            _context = dapperContext;
        }

        public async Task<int> AddAsync(TransactionForCreationDto trans)
        {
            var sp = "CreateTransaction";

            var param = new DynamicParameters();

            param.Add("@AccountId", trans.AccountId);
            param.Add("@Type", trans.Type);
            param.Add("@Operation", trans.Operation);
            param.Add("@Amount", trans.Amount);
            param.Add("@Balance", trans.Balance);

            using (var db = _context.CreateConnection())
            {
                var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

                return id;
            }
        }
    }
}
