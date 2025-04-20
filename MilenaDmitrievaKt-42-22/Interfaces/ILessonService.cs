using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ILessonsService
    {
        public Task<Lessons[]> GetLessonsAsync(LessonsFilter filter, CancellationToken cancellationToken);
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

    }
}
