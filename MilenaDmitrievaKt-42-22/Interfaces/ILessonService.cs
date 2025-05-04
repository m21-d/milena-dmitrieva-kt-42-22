using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ILessonsService
    {
        public Task<Lessons[]> GetLessonsAsync(LessonsFilter filter, CancellationToken cancellationToken);
        public Task<int> UpdateLessonsAsync(LessonsUpdate t, CancellationToken cancellationToken);
        public Task<int> AddLessonsAsync(LessonsAdd t, CancellationToken cancellationToken);
    }

    public class LessonsService : ILessonsService
    {
        private readonly TeachersDbContext _dbContext;
        public LessonsService(TeachersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Lessons[]> GetLessonsAsync(LessonsFilter filter, CancellationToken cancellationToken = default)
        {
            var a = _dbContext.Set<Lessons>().Where(w =>
                (filter.TeacherSurname == null || w.Teacher.Surname == filter.TeacherSurname) &&
                (filter.CafedraName == null || w.Teacher.Cafedra.CafedraName == filter.CafedraName) &&
                (filter.SubjectName == null || w.Subject.SubjectName == filter.SubjectName)).Include("Teacher.Cafedra.Head").Select(t => new Lessons
                {
                    LessonsId = t.LessonsId,
                    Hours = t.Hours,
                    SubjectId = t.SubjectId,
                    Subject = t.Subject,
                    TeacherId = t.TeacherId,
                    Teacher = new Teacher
                    {
                        TeacherId = t.Teacher.TeacherId,
                        Surname = t.Teacher.Surname,
                        Name = t.Teacher.Name,
                        Patronym = t.Teacher.Patronym,
                        CafedraId = t.Teacher.CafedraId,
                        Cafedra = new Cafedra
                        {
                            CafedraId = t.Teacher.Cafedra.CafedraId,
                            CafedraName = t.Teacher.Cafedra.CafedraName,
                            Year = t.Teacher.Cafedra.Year,
                            HeadId = t.Teacher.Cafedra.HeadId,
                            Head = t.Teacher.Cafedra.Head == null ? null : new Teacher
                            {
                                TeacherId = t.Teacher.Cafedra.Head.TeacherId,
                                Surname = t.Teacher.Cafedra.Head.Surname,
                                Name = t.Teacher.Cafedra.Head.Name,
                                Patronym = t.Teacher.Cafedra.Head.Patronym,
                                CafedraId = t.Teacher.Cafedra.Head.CafedraId,
                                DegreeId = t.Teacher.Cafedra.Head.DegreeId,
                                Degree = t.Teacher.Cafedra.Head.Degree,
                                PositionId = t.Teacher.Cafedra.Head.PositionId,
                                Position = t.Teacher.Cafedra.Head.Position
                            }
                        },
                        DegreeId = t.Teacher.DegreeId,
                        Degree = t.Teacher.Degree,
                        PositionId = t.Teacher.PositionId,
                        Position = t.Teacher.Position
                    }

                })
                .ToArrayAsync(cancellationToken);
            return a;
        }
        public Task<int> UpdateLessonsAsync(LessonsUpdate t, CancellationToken cancellationToken = default)
        {
            var b = _dbContext.Set<Lessons>().Where(e => e.LessonsId == t.LessonsId);
            if (b.Any())
            {
                var a = b.First();
                if (t.TeacherId != null)
                    a.TeacherId = (int)t.TeacherId;
                if (t.SubjectId != null)
                    a.SubjectId = (int)t.SubjectId;
                if (t.Hours != null)
                    a.Hours = (int)t.Hours;
            }

            var a1 = _dbContext.SaveChangesAsync(cancellationToken);
            return a1;
        }
        public Task<int> AddLessonsAsync(LessonsAdd t, CancellationToken cancellationToken = default)
        {

            Lessons t1 = new Lessons
            {
                TeacherId = t.TeacherId,
                SubjectId = t.SubjectId,
                Hours = t.Hours
            };

            _dbContext.Set<Lessons>().Add(t1);

            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
    }
}
