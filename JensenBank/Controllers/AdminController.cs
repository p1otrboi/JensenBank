using JensenBank.Core.Dto;
using JensenBank.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Domain;

namespace JensenBank.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;

        public AdminController(ICustomerService customerService, IAdminService adminService)
        {
            _customerService = customerService;
            _adminService = adminService;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> AddCustomer(CustomerForCreationDto customer)
        {
            try
            {
                //var createdCustomer = await _customerService.AddAsync(customer);

                //return Ok(createdCustomer);

                LoginRequestDto userinfo = new()
                { Username = "testing", Password= "testing" };

                var result = await _adminService.CreateCustomer(customer, userinfo);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
