using JensenBank.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Domain;

namespace JensenBank.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public AdminController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> AddCustomer(CustomerForCreationDto customer)
        {
            try
            {
                var createdCustomer = await _customerService.AddAsync(customer);

                return Ok(createdCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
