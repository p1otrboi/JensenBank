using JensenBank.Application.Services;
using JensenBank.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JensenBank.WebApi.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public CustomerController(IUserService userService, ICustomerService customerService)
        {
            _userService = userService;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountSummary()
        {
            try
            {
                int id = _userService.GetCustomerId();

                var result = await _customerService.GetAccountSummary(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("account")]
        public async Task<IActionResult> CreateAccount(AccountForCreationDto details)
        {
            try
            {
                int customerId = _userService.GetCustomerId();

                var result = await _customerService.CreateAccount(customerId, details);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetAccountTransactions(int accountId)
        {
            try
            {
                int customerId = _userService.GetCustomerId();

                var result = await _customerService.GetAccountWithTransactions(customerId, accountId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferMoney(MoneyTransferDto details)
        {
            try
            {
                int customerId = _userService.GetCustomerId();

                var result = await _customerService.TransferMoney(customerId, details);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
