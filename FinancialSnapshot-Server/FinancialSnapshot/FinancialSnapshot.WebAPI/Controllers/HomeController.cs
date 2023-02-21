using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSnapshot.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger): base(logger) { }
    }
}
