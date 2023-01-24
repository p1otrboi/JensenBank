using JensenBank.Application.Services;
using JensenBank.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Domain;

namespace JensenBank.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("customer")]
    public async Task<IActionResult> AddCustomer(CustomerForCreationDto customer)
    {
        try
        {
            var result = await _adminService.CreateCustomer(customer);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("loan")]
    public async Task<IActionResult> CreateLoan(LoanForCreationDto loan)
    {
        try
        {
            var result = await _adminService.CreateLoan(loan);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
