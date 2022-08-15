using FinancialSnapshot.Abstraction.Services;
using FinancialSnapshot.Models.Configuration;
using FinancialSnapshot.Models.Web.General;
using FinancialSnapshot.Models.Web.Security;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSnapshot.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IUserService _service;

        public SecurityController(ILogger<SecurityController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] BaseDataRequest<LoginRequest> request)
        {
            try
            {
                var response = new BaseDataResponse<string>();
                if (request.Data == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid credentials!";
                    return Ok(response);
                }

                response = await _service.ProcessLogin(request.Data.Username, request.Data.Password);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Invalid request");
            }            
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] BaseDataRequest<RegisterRequest> request)
        {
            try
            {
                var response = new BaseDataResponse<bool>();
                if (request.Data == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid credentials!";
                    return Ok(response);
                }

                response = await _service.ProcessRegister(request.Data);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Invalid request!");
            }
        }
    }
}