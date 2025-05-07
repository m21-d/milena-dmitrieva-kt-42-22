using System.Text.RegularExpressions;

namespace MilenaDmitrievaKt_42_22.Models
{
    public class Degree
    {
        public int DegreeId {get; set;}
        public string DegreeName { get; set;}

        public bool IsValidDegree()
        {
            return Regex.Match(DegreeName, @"^[а-я]. [а-я]. [а-я].$").Success;
        }
    }
}
