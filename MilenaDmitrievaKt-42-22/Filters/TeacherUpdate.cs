using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Filters
{
    public class TeacherUpdate
    {
        public int TeacherId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronym { get; set; }
        public int? CafedraId { get; set; }
        public int? DegreeId { get; set; }
        public int? PositionId { get; set; }
    }
}
