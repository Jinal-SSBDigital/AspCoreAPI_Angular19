namespace BSEB_CoreAPI.Model
{
    public class InterRegiRequest
    {
        public List<int>? StudentIds { get; set; } = new();

        public int? CollegeId { get; set; }
        public int? FacultyId { get; set; }
     }
}
