using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ISubjectService
    {
        public Task<Subject[]> GetSubjectsAsync(SubjectFilter filter, CancellationToken cancellationToken);
        public Task<int> DeleteSubjectAsync(int id, CancellationToken cancellationToken);
        public Task<int> UpdateSubjectAsync(SubjectUpdate t, CancellationToken cancellationToken);
        public Task<int> AddSubjectAsync(SubjectAdd t, CancellationToken cancellationToken);
    }

    public class SubjectService : ISubjectService
    {
        private readonly TeachersDbContext _dbContext;
        public SubjectService(TeachersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Subject[]> GetSubjectsAsync(SubjectFilter filter, CancellationToken cancellationToken = default)
        {
            var t = _dbContext.Set<Teacher>();
            var l = _dbContext.Set<Lessons>().Join(t, l1 => l1.TeacherId, t1 => t1.TeacherId, (l2, t2) => new
            {
                t2.TeacherId,
                t2.Surname,
                l2.LessonsId,
                l2.Hours,
                l2.SubjectId
            });
            var a = _dbContext.Set<Subject>().GroupJoin(l, s => s.SubjectId, l1 => l1.SubjectId, (s2, l2) => new
            {
                s2,
                hoursSum = l2.Sum(e => e.Hours),
                ts=l2.Where(e => (filter.TeacherSurname == null) || (e.Surname == filter.TeacherSurname)).Any(),
            }).Where(e =>
            ((filter.HoursMax == null) || (e.hoursSum <= filter.HoursMax)) &&
            ((filter.HoursMin == null) || (e.hoursSum >= filter.HoursMin)))
            .Where(e => (filter.TeacherSurname == null) || (e.ts))
            .Select(e => e.s2)
             .ToArrayAsync(cancellationToken);
            return a;
        }
        public Task<int> DeleteSubjectAsync(int id, CancellationToken cancellationToken = default)
        {
            
            var t = _dbContext.Set<Subject>().Find(id);
            if (t != null)
                _dbContext.Set<Subject>().Remove(t);
            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
        public Task<int> UpdateSubjectAsync(SubjectUpdate t, CancellationToken cancellationToken = default)
        {
            var b = _dbContext.Set<Subject>().Where(e => e.SubjectId == t.SubjectId);
            if (b.Any())
            {
                var a = b.First();
                if (t.SubjectName != null)
                    a.SubjectName = t.SubjectName;
            }

            var a1 = _dbContext.SaveChangesAsync(cancellationToken);
            return a1;
        }
        public Task<int> AddSubjectAsync(SubjectAdd t, CancellationToken cancellationToken = default)
        {

            Subject t1 = new Subject
            {
                SubjectName = t.SubjectName
            };

            _dbContext.Set<Subject>().Add(t1);

            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
    }
}
