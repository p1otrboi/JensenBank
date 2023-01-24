using JensenBank.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Infrastructure.Interfaces
{
    public interface ITransactionRepo
    {
        public Task<int> AddAsync(TransactionForCreationDto transaction);
    }
}
