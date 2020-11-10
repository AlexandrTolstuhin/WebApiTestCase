using Microsoft.AspNetCore.Mvc;

namespace WebApiTestCase.Api.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    { }
}