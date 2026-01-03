namespace BSEB_CoreAPI.Model
{
    public class StudentInterRegiSpDto
    {
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? NewDOB { get; set; }
       public string? CategoryName { get; set; }
     
        public int? FacultyId { get; set; }
       
        public string? ApaarId { get; set; }
        public string? BSEB_Unique_ID { get; set; }
     
        public bool? FormDownloaded { get; set; }
        public string? FacultyName { get; set; }
       

        public string? OFSSCAFNo { get; set; }
       
        public string? CollegeCode { get; set; }
        public string? CollegeName { get; set; }

        public string? DistrictName { get; set; }

        public string? MatricRollCode { get; set; }
        public string? MatricRollNumber { get; set; }
        public string? MatricPassingYear { get; set; }

        public string? Gender { get; set; }
        public string? MatricBoardName { get; set; }
        public string? CasteCategory { get; set; }
        public bool? DifferentlyAbled { get; set; }

        public string? AadharNumber { get; set; }
        public int? Fk_NationalityId { get; set; }
        public string? Religion { get; set; }
        public string? Nationality { get; set; }

        public string? SubDivisionName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? StudentAddress { get; set; }

        public string? AreaName { get; set; }
        public string? MaritalStatus { get; set; }

        public string? StudentBankAccountNo { get; set; }
        public string? BankBranchName { get; set; }
        public string? IFSCCode { get; set; }

        public string? IdentificationMark1 { get; set; }
        public string? IdentificationMark2 { get; set; }
        public string? MediumName { get; set; }

        public string? StudentPhotoPath { get; set; }
        public string? StudentSignaturePath { get; set; }
    }
}
