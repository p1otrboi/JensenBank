using Models.Domain;

namespace JensenBank.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<int> AddAsync(CustomerForCreationDto customer);
        public Task<Customer> GetByIdAsync(int id);
    }
}
