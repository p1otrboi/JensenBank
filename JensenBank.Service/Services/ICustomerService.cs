using Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Service.Services
{
    public interface ICustomerService
    {
        public Task<Customer> AddAsync(CustomerForCreationDto customer);
    }
}
