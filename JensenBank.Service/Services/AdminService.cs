using JensenBank.Core.Dto;
using JensenBank.Repository.Authentication;
using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Service.Services;

public class AdminService : IAdminService
{
    private readonly ICustomerRepo _customerRepo;
    private readonly ITransactionRepo _transactionRepo;
    private readonly IUserRepo _userRepo;
    private readonly IAccountRepo _accountRepo;
    private readonly ILoanRepo _loanRepo;
    private readonly IDispositionRepo _dispositionRepo;
    private readonly IPasswordEncryption _pwEncryption;

    public AdminService(ICustomerRepo customerRepo, ITransactionRepo transactionRepo,
        IUserRepo userRepo, IAccountRepo accountRepo, 
        IDispositionRepo dispositionRepo, IPasswordEncryption pwEncryption, ILoanRepo loanRepo)
    {
        _customerRepo = customerRepo;
        _userRepo = userRepo;
        _accountRepo = accountRepo;
        _dispositionRepo = dispositionRepo;
        _pwEncryption = pwEncryption;
        _loanRepo = loanRepo;
        _transactionRepo = transactionRepo;
    }

    public async Task<CreatedCustomerDto> CreateCustomer(CustomerForCreationDto customer)
    {
        var customerId = await _customerRepo.AddAsync(customer);

        AccountForCreationDto details = new()
        {
            Frequency = "Monthly",
            AccountTypeId = 1
        };

        var accountId = await _accountRepo.AddAsync(details);

        await _dispositionRepo.AddAsync(customerId, accountId, "OWNER");

        var hash = _pwEncryption.HashPassword(customer.Desired_Password, out byte[] salt);

        DbUserForCreationDto user = new()
        {
            CustomerId = customerId,
            Username = customer.Desired_Username,
            PW_Hash = hash,
            PW_Salt = Convert.ToHexString(salt),
            User_RoleId = 2
        };

        await _userRepo.AddAsync(user);

        var account = await _accountRepo.GetByIdAsync(accountId);

        var createdCustomer = await _customerRepo.GetByIdAsync(customerId);

        CreatedCustomerDto result = new()
        {
            Customer_Details = createdCustomer,
            Account = account,
            Username = customer.Desired_Username,
            Password = customer.Desired_Password
        };

        return result;
    }

    public async Task<Loan> CreateLoan(LoanForCreationDto loan)
    {
        var id = await _loanRepo.AddAsync(loan);

        await _accountRepo.AddAmountToAccountBalanceAsync(loan.AccountId, loan.Amount);

        var balance = await _accountRepo.GetBalanceAsync(loan.AccountId);

        TransactionForCreationDto trans = new()
        {
            AccountId = loan.AccountId,
            Type = "Credit",
            Operation = "Credit in Cash",
            Amount = loan.Amount,
            Balance = balance
        };

        await _transactionRepo.AddAsync(trans);

        var result = await _loanRepo.GetByIdAsync(id);

        return result;
    }
}
