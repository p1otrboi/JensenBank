using JensenBank.Application.Services;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Interfaces;
using Microsoft.Identity.Client;
using Models.Domain;
using Moq;

namespace JensenBank.UnitTests
{
    public class CustomerServiceTest
    {
        private readonly CustomerService _sut;
        private readonly Mock<IAccountRepo> _accountRepoMock = new Mock<IAccountRepo>();
        private readonly Mock<IDispositionRepo> _dispositionRepoMock = new Mock<IDispositionRepo>();
        private readonly Mock<ITransactionRepo> _transactionRepoMock = new Mock<ITransactionRepo>();
        private readonly Mock<ICustomerRepo> _customerRepoMock = new Mock<ICustomerRepo>();

        public CustomerServiceTest()
        {
            _sut = new CustomerService(_customerRepoMock.Object, _accountRepoMock.Object, _dispositionRepoMock.Object, _transactionRepoMock.Object);
        }

        [Fact]
        public async Task GetAccountSummary_ShouldReturn_ListOf_AccountSummaryDto()
        {
            var customerId = 1;
            var accountSummaryDto = new List<AccountSummaryDto>();
            
            // Arrange
            _accountRepoMock.Setup(x => x.GetAccountSummary(customerId))
                .ReturnsAsync(accountSummaryDto);

            // Act
            var result = await _sut.GetAccountSummary(customerId);

            // Assert
            Assert.Equal(accountSummaryDto, result);
        }

        [Fact]
        public async Task GetAccountWithTransactions_ShouldReturn_MoneyTransferDto()
        {
            int customerId = 1;
            int accountId = 1;
            var account = new List<AccountSummaryDto>()
            {
                new AccountSummaryDto
                {
                    AccountId = 1
                }
            };
            var accountTransactionsDto = new AccountTransactionsDto();

            _accountRepoMock.Setup(x => x.GetAccountWithTransactions(accountId))
                .ReturnsAsync(accountTransactionsDto);
            _accountRepoMock.Setup(x => x.GetAccountSummary(customerId))
                .ReturnsAsync(account);

            var result = await _sut.GetAccountWithTransactions(customerId, accountId);

            Assert.Equal(accountTransactionsDto, result);
        }

        [Fact]
        public async Task CreateAccount_ShouldReturn_ListOf_AccountSummaryDto()
        {
            int accountId = 1;
            int customerId = 1;
            var accountSummaryDto = new List<AccountSummaryDto>();
            var mockDetails = new AccountForCreationDto()
            {
                AccountTypeId = 1,
                Frequency = "Monthly"
            };

            // Arrange
            _accountRepoMock.Setup(x => x.AddAsync(mockDetails))
                .ReturnsAsync(accountId);
            _accountRepoMock.Setup(x => x.GetAccountSummary(customerId))
              .ReturnsAsync(accountSummaryDto);

            // Act
            var result = await _sut.CreateAccount(customerId, mockDetails);

            // Assert
            Assert.Equal(accountSummaryDto, result);
        }

        [Fact]
        public async Task TransferMoney_ShouldReturn_MoneyTransferDto()
        {
            int customerId = 1;
            var recipentAccount = new Account()
            {
                Frequency = "Monthly"
            };
            var accountSummaryDto = new List<AccountSummaryDto>()
            {
                new AccountSummaryDto
                {
                    AccountId = customerId,
                    Balance = 100
                }
            };
            var mockDetails = new MoneyTransferDto()
            {
                From_Account = 1,
                To_Account = 2,
                Amount = 100
            };

            _accountRepoMock.Setup(x => x.GetAccountSummary(customerId))
                .ReturnsAsync(accountSummaryDto);
            _accountRepoMock.Setup(x => x.GetByIdAsync(mockDetails.To_Account))
                .ReturnsAsync(recipentAccount);
            _accountRepoMock.Setup(x => x.SubAmountFromAccountBalanceAsync(mockDetails.From_Account, mockDetails.Amount))
                .ReturnsAsync(100);
            _accountRepoMock.Setup(x => x.AddAmountToAccountBalanceAsync(mockDetails.From_Account, mockDetails.Amount))
                .ReturnsAsync(100);

            var result = await _sut.TransferMoney(customerId, mockDetails);

            Assert.Equal(mockDetails, result);
        }
    }
}