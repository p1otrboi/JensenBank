using JensenBank.Core.Dto;
using Models.Domain;

namespace JensenBank.Repository.Interfaces
{
    public interface ILoanRepo
    {
        public Task<Loan> GetByIdAsync(int id);
        public Task<int> AddAsync(LoanForCreationDto loan);
    }
}
