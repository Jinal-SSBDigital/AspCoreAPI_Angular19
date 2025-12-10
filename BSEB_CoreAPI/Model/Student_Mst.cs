using System.ComponentModel.DataAnnotations;

namespace BSEB_CoreAPI.Model
{
    public class Student_Mst
    {
        [Key]
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateTime? DOB { get; set; }
        //public string CategoryName { get; set; }
        public string? Faculty { get; set; }
        public int? FacultyId { get; set; }
        public int? CollegeId { get; set; }
        public string? College { get; set; }
        //public string OfssReferenceNo { get; set; }
        public bool? FormDownloaded { get; set; }

    }
}
