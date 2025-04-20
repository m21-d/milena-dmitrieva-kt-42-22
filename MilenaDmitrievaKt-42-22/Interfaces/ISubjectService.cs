using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ISubjectService
    {
        public Task<Subject[]> GetSubjectsAsync(SubjectFilter filter, CancellationToken cancellationToken);
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
            }).Where(e => (filter.TeacherSurname == null) || (e.Surname == filter.TeacherSurname));
            var a = _dbContext.Set<Subject>().GroupJoin(l, s => s.SubjectId, l1 => l1.SubjectId, (s2, l2) => new
            {
                s2,
                hoursSum = l2.Sum(e => e.Hours),
            }).Where(e =>
            ((filter.HoursMax == null) || (e.hoursSum < filter.HoursMax)) &&
            ((filter.HoursMin == null) || (e.hoursSum > filter.HoursMin)))
            .Select(e => e.s2)
             .ToArrayAsync(cancellationToken);
            return a;
        }

    }
}
