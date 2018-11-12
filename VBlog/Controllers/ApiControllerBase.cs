using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace VBlog.Controllers
{
    [EnableCors("AllowSameDomain")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : Controller
    {
    }
}
