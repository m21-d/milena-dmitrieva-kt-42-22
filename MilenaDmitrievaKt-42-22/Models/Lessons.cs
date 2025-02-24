namespace MilenaDmitrievaKt_42_22.Models
{
    public class Lessons
    {
        public int LessonsId {  get; set; }
        public int TeacherId {  get; set; }
        public Teacher Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Hours { get; set; }
    }
}
