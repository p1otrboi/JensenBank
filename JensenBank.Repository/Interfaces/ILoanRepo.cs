using JensenBank.Core.Dto;
using Models.Domain;

namespace JensenBank.Infrastructure.Interfaces
{
    public interface ILoanRepo
    {
        public Task<Loan> GetByIdAsync(int id);
        public Task<int> AddAsync(LoanForCreationDto loan);
    }
}
