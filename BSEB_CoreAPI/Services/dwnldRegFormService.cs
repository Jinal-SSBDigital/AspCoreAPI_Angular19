using BSEB_CoreAPI.Data;
using BSEB_CoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace BSEB_CoreAPI.Services
{
    public class dwnldRegFormService : IdwnldRegFormService
    {
        private readonly IConfiguration _configuration; 
        private readonly AppDbContext _context;

        public dwnldRegFormService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<Student_Mst>> GetStudentDetails(string CollegeId, string CollegeCode, string StudentName, string FacultyId)
        {
            try
            {
              
                var collegeIdParam = new SqlParameter("@CollegeId", string.IsNullOrWhiteSpace(CollegeId) ? DBNull.Value : CollegeId);
                var collegeCodeParam = new SqlParameter("@CollegeCode",string.IsNullOrWhiteSpace(CollegeCode) ? DBNull.Value : CollegeCode);
                var studentNameParam = new SqlParameter("@StudentName", string.IsNullOrWhiteSpace(StudentName) ? "" : StudentName);
                var facultyIdParam = new SqlParameter("@FacultyId",string.IsNullOrWhiteSpace(FacultyId) ? DBNull.Value : FacultyId);
                var subCategoryParam = new SqlParameter("@SubCategory", DBNull.Value);

                var result = await _context.Student_Mst.FromSqlRaw("EXEC sp_GetStudentDetails @CollegeId, @CollegeCode, @StudentName, @FacultyId, @SubCategory",collegeIdParam, collegeCodeParam, studentNameParam, facultyIdParam, subCategoryParam).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<List<Student_Mst>> GetStudentDetails(string Collegeid, string CollegeCode, string StudentName, string FacultyId)
        //{
        //    //throw new NotImplementedException();
        //    try
        //    {
        //        var CollegeIdPr=new SqlParameter("@CollegeId", Collegeid);
        //        var CollegCodePr=new SqlParameter("@CollegeCode", (object?)CollegeCode ?? DBNull.Value);
        //        var StudentNamePr= new SqlParameter("@StudentName", (object?)StudentName ?? DBNull.Value);
        //        var FacultyIdPr = new SqlParameter("@FacultyId", FacultyId);
        //        var SubCategory = new SqlParameter("@SubCategory", DBNull.Value);

        //        //var result = _context.StudentDetails.FromSqlRaw("EXEC sp_GetStudentDetails @CollegeId, @CollegeCode, @StudentName, @FacultyId,@SubCategory", CollegeIdPr, CollegCodePr, StudentNamePr, FacultyIdPr, SubCategory).AsEnumerable().FirstOrDefault();
        //        var result = await _context.Student_Mst.FromSqlRaw("EXEC sp_GetStudentDetails @CollegeId, @CollegeCode, @StudentName, @FacultyId,@SubCategory", CollegeIdPr, CollegCodePr, StudentNamePr, FacultyIdPr, SubCategory).ToListAsync();

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
