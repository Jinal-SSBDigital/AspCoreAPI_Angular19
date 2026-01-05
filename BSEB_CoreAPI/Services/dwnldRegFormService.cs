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

        public async Task<List<StudentWithSubjectsDto>> InterRegistrationForm(InterRegiRequest InterRegi)
        {
            try
            {
                var result= new List<StudentWithSubjectsDto>();

                // ---------- SUBJECTS ----------
                var Subjects = await _context.SubjectPaperEntities.FromSqlRaw("EXEC sp_GetSubjectPapersByFacultyAndGroup @FacultyId",new SqlParameter("@FacultyId",InterRegi.FacultyId)).ToListAsync();

                // ---------- STUDENT ----------
                foreach (var StudentId in InterRegi.StudentIds)
                {
                    var students = await _context.studentInterRegiSps.FromSqlRaw("EXEC GetStudentInterRegiFormData @StudentID,@CollegeId,@FacultyId",
                    new SqlParameter("@StudentID", StudentId),
                     new SqlParameter("@CollegeId", InterRegi.CollegeId),
                    new SqlParameter("@FacultyId", InterRegi.FacultyId)).ToListAsync();

                    result.AddRange(students.Select(s => new StudentWithSubjectsDto
                    {
                       
                        Student = new StudentInterRegiSpDto
                        {
                            StudentName = s.StudentName,
                            FatherName = s.FatherName,
                            MotherName = s.MotherName,
                            DOB = s.DOB,
                            NewDOB = s.NewDOB,
                            CategoryName = s.CategoryName,
                            FacultyId = s.FacultyId,
                            ApaarId = s.ApaarId,
                            BSEB_Unique_ID = s.BSEB_Unique_ID,
                            FormDownloaded = s.FormDownloaded,
                            FacultyName = s.FacultyName,
                            OFSSCAFNo = s.OFSSCAFNo,
                            CollegeCode = s.CollegeCode,
                            CollegeName = s.CollegeName,
                            DistrictName = s.DistrictName,
                            MatricRollCode = s.MatricRollCode,
                            MatricRollNumber = s.MatricRollNumber,
                            MatricPassingYear = s.MatricPassingYear,
                            Gender = s.Gender,
                            MatricBoardName = s.MatricBoardName,
                            CasteCategory = s.CasteCategory,
                            DifferentlyAbled = s.DifferentlyAbled,
                            AadharNumber = s.AadharNumber,
                            Fk_NationalityId = s.Fk_NationalityId,
                            Religion = s.Religion,
                            Nationality = s.Nationality,
                            SubDivisionName = s.SubDivisionName,
                            MobileNo = s.MobileNo,
                            EmailId = s.EmailId,
                            StudentAddress = s.StudentAddress,
                            AreaName = s.AreaName,
                            MaritalStatus = s.MaritalStatus,
                            StudentBankAccountNo = s.StudentBankAccountNo,
                            BankBranchName = s.BankBranchName,
                            IFSCCode = s.IFSCCode,
                            IdentificationMark1 = s.IdentificationMark1,
                            IdentificationMark2 = s.IdentificationMark2,
                            MediumName = s.MediumName,
                            StudentPhotoPath = s.StudentPhotoPath,
                            StudentSignaturePath = s.StudentSignaturePath
                        },
                        Subjects = Subjects
                    }));
                }
             

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
    
        }

       
    }
}
