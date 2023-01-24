using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Infrastructure.Interfaces
{
    public interface IDispositionRepo
    {
        public Task<int> AddAsync(int customerId, int accountId, string type);
    }
}
