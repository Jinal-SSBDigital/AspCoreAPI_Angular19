using BSEB_CoreAPI.Model;
using BSEB_CoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSEB_CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAuth : ControllerBase
    {
        private readonly ILoginService _LoginService;
        //private readonly ILogger<LoginAuth> _logger;

        public LoginAuth(ILoginService LoginService)
        {
            _LoginService = LoginService;
            //_logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var result = await _LoginService.LoginUserAsync(loginRequest.Username, loginRequest.Password);

                if (!result.IsSuccess)
                {
                    return Unauthorized(new { Message = result.Message });
                }

                // If login is successful, return the user data and any other relevant information (e.g., token)
                return Ok(new
                {
                    Message = result.Message,
                    CollegeId = result.CollegeId,
                    CollegeName = result.CollegeName,
                    CollegeCode = result.CollegeCode,
                    DistrictName = result.DistrictName,
                    DistrictCode = result.DistrictCode,
                    PrincipalMobileNo = result.PrincipalMobileNo,
                    EmailId = result.EmailId
                });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Exception during login for user: {Username}", request.Username);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
