using System.ComponentModel.DataAnnotations;

namespace BSEB_CoreAPI.Model
{
    public class Faculty_Mst
    {
        //[Key]
        public int Pk_FacultyId { get; set; }

        public string? FacultyName { get; set; }

        public bool? IsActive { get; set; }
    }
}
