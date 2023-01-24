using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JensenBank.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/user")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

    }
}
