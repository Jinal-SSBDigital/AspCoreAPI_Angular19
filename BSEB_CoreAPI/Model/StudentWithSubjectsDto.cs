using System.ComponentModel.DataAnnotations;

namespace BSEB_CoreAPI.Model
{
    public class StudentWithSubjectsDto
    {
        public StudentInterRegiSpDto Student { get; set; }

        public List<SubjectPaperDto> Subjects { get; set; } = new();
    }

    public class SubjectPaperDto
    {

        public int? Pk_SubjectPaperId { get; set; }
        public int? Fk_SubjectGroupId { get; set; }
        public string? SubjectPaperName { get; set; }
        public int? SubjectPaperCode { get; set; }
        public string? GroupName { get; set; }
        public string? GroupNameHindi { get; set; }
    }
}
