using JensenBank.Core.Dto;
using Models.Domain;

namespace JensenBank.Service.Services
{
    public interface IAdminService
    {
        public Task<CreatedCustomerDto> CreateCustomer(CustomerForCreationDto customer);
    }
}
