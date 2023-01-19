using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo cuistomerRepo)
        {
            _customerRepo = cuistomerRepo;
        }

        public async Task<Customer> AddAsync(CustomerForCreationDto c)
        {
            int createdCustomerId = await _customerRepo.AddAsync(c);

            var createdCustomer = await _customerRepo.GetByIdAsync(createdCustomerId);

            return createdCustomer;
        }
    }
}
