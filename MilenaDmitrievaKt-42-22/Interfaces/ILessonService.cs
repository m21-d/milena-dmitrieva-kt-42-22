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
                (filter.SubjectName == null || w.Subject.SubjectName == filter.SubjectName))
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
