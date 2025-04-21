using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Filters
{
    public class LessonsUpdate
    {
        public int LessonsId { get; set; }
        public int? TeacherId { get; set; }
       
        public int? SubjectId { get; set; }
        
        public int? Hours { get; set; }
    }
}
