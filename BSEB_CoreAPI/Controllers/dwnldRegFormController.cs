using BSEB_CoreAPI.Model;
using BSEB_CoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSEB_CoreAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class dwnldRegFormController : ControllerBase
    {
        private readonly IdwnldRegFormService _dwnldRegFormService;
        public dwnldRegFormController(IdwnldRegFormService dwnldRegFormService)
        {
            _dwnldRegFormService = dwnldRegFormService;
        }
        [AllowAnonymous]
        [HttpPost("dwnldRegForm")]
        public async Task<IActionResult> dwnldRegForm([FromBody]StudentRequest Stu)
        {
            try
            {
                var result = await _dwnldRegFormService.GetStudentDetails(Stu.CollegeId.ToString(),Stu.CollegeCode,Stu.StudentName,Stu.FacultyId.ToString());
                return Ok(new { success = true, data = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
          
        }
    }
}
