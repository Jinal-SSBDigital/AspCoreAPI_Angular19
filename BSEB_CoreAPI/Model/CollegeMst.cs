namespace BSEB_CoreAPI.Model
{
    public class CollegeMst
    {
        public int Pk_CollegeId { get; set; }
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalMobileNo { get; set; }
        public int Fk_BlockId { get; set; }
        public int Fk_DistrictId { get; set; }
        public string EmailId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
