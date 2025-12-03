namespace BSEB_CoreAPI.Model
{
    public class CollegeUser
    {
        public int Id { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Fk_CollegeId { get; set; }
        public string PlainText_Password { get; set; } // Assuming you store the plain text password temporarily (not recommended for production)
    }

}
