﻿namespace MilenaDmitrievaKt_42_22.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronym { get; set; }
        public int CafedraId {  get; set; }
        public Cafedra Cafedra { get; set; }
        public int DegreeId {  get; set; }
        public Degree Degree { get; set; }
        public int PositionId {  get; set; }
        public Position Position { get; set; }

        public int? HCafedraId { get; set; }
        public Cafedra? HCafedra { get; set; }
    }
}
