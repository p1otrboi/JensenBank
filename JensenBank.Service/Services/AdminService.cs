﻿using JensenBank.Core.Dto;
using JensenBank.Repository.Authentication;
using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Service.Services;

public class AdminService : IAdminService
{
    private readonly ICustomerRepo _customerRepo;
    private readonly IUserRepo _userRepo;
    private readonly IAccountRepo _accountRepo;
    private readonly IDispositionRepo _dispositionRepo;
    private readonly IPasswordEncryption _pwEncryption;

    public AdminService(ICustomerRepo customerRepo, 
        IUserRepo userRepo, IAccountRepo accountRepo, 
        IDispositionRepo dispositionRepo, IPasswordEncryption pwEncryption)
    {
        _customerRepo = customerRepo;
        _userRepo = userRepo;
        _accountRepo = accountRepo;
        _dispositionRepo = dispositionRepo;
        _pwEncryption = pwEncryption;
    }

    public async Task<CreatedCustomerDto> CreateCustomer(CustomerForCreationDto customer, LoginRequestDto userinfo)
    {
        var customerId = await _customerRepo.AddAsync(customer);

        AccountForCreationDto details = new()
        {
            Frequency = "Monthly",
            AccountTypeId = 1
        };

        var accountId = await _accountRepo.AddAsync(details);

        await _dispositionRepo.AddAsync(customerId, accountId, "OWNER");

        var hash = _pwEncryption.HashPassword(userinfo.Password, out byte[] salt);

        DbUserForCreationDto user = new()
        {
            CustomerId = customerId,
            Username = userinfo.Username,
            PW_Hash = hash,
            PW_Salt = Convert.ToHexString(salt),
            User_RoleId = 2
        };

        var userId = await _userRepo.AddAsync(user);

        var createdCustomer = await _customerRepo.GetByIdAsync(customerId);
        var createdUser = await _userRepo.GetByUsername(user.Username);

        CreatedCustomerDto result = new()
        {
            Customer_Details = createdCustomer,
            User_Details = createdUser
        };

        return result;
    }
}
