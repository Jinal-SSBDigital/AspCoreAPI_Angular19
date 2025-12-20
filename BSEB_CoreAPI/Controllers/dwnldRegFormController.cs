using BSEB_CoreAPI.Data;
using BSEB_CoreAPI.Model;
using BSEB_CoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using System.Data.Entity;

namespace BSEB_CoreAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class dwnldRegFormController : ControllerBase
    {
        private readonly IdwnldRegFormService _dwnldRegFormService;
        private readonly AppDbContext _context;
        public dwnldRegFormController(IdwnldRegFormService dwnldRegFormService, AppDbContext context)
        {
            _dwnldRegFormService = dwnldRegFormService;
            _context = context;

        }

        [AllowAnonymous]
        [HttpGet("bindfaculty")]
        public async Task<IActionResult> GetFaculty()
        {
            try
            {
                var faculties = await _context.Faculty_Mst.Where(f => f.IsActive == true).ToListAsync();
                return Ok(new { success = true, data = faculties });
                //return Ok(faculties);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        [HttpPost("dwnldRegForm")]
        public async Task<IActionResult> dwnldRegForm([FromBody] StudentRequest Stu)
        {
            try
            {
                //if (Stu.CollegeId == "0")
                //{
                //    Stu.CollegeId == "";
                //}
                var result = await _dwnldRegFormService.GetStudentDetails("", Stu.CollegeCode, Stu.StudentName, Stu.faculty.ToString());
                return Ok(new { success = true, data = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }

        }

        [AllowAnonymous]
        [HttpPost("InterRegistrationForm")]
        public async Task<IActionResult> InterRegistrationForm([FromForm] InterRegiRequest interRegi)
        {
            try
            {
                var result = await _dwnldRegFormService.InterRegistrationForm(interRegi);
                var pdf = new InterRegistrationPdf(result);
                var pdfBytes = pdf.GeneratePdf();

                return File(
                    pdfBytes,
                    "application/pdf",
                    "InterRegistrationForm.pdf"
                );
                //return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
