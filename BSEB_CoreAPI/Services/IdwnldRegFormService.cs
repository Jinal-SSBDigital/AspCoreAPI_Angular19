using BSEB_CoreAPI.Model;

namespace BSEB_CoreAPI.Services
{
    public interface IdwnldRegFormService
    {
        Task <List<Student_Mst>> GetStudentDetails(string Collegeid,String CollegeCode,string StudentName,string FacultyId);
        Task <List<Student_Mst>> InterRegistrationForm(InterRegiRequest InterRegi);
    }
}
