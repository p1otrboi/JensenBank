﻿using JensenBank.Core.Dto;
using Models.Domain;

namespace JensenBank.Infrastructure.Interfaces
{
    public interface IAccountRepo
    {
        public Task<Account> GetByIdAsync(int id);
        public Task<int> AddAsync(AccountForCreationDto account);
        public Task<int> SetAccountType(int accountId, int accountTypeId);
        public Task<decimal> AddAmountToAccountBalanceAsync(int accountId, decimal amount);
        public Task<decimal> GetBalanceAsync(int accountId);
        public Task<List<AccountSummaryDto>> GetAccountSummary(int customerId);
    }
}
