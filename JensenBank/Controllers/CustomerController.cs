using JensenBank.Repository.Interfaces;
using JensenBank.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JensenBank.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountRepo _accountRepo;

        public CustomerController(IUserService userService, IAccountRepo accountRepo)
        {
            _userService = userService;
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountSummary()
        {
            try
            {
                int id = _userService.GetCustomerId();

                var result = await _accountRepo.GetAccountSummary(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
