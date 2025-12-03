namespace BSEB_CoreAPI.Model
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string CollegeCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string PrincipalMobileNo { get; set; }
        public string EmailId { get; set; }
    }
}
