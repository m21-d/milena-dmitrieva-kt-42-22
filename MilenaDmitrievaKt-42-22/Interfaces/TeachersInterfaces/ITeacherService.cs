using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters.TeacherFilters;
using MilenaDmitrievaKt_42_22.Models;
using Microsoft.EntityFrameworkCore;
namespace MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces
{
    public interface ITeacherService
    {
        public Task<Teacher[]> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeachersDbContext _dbContext;
        public TeacherService(TeachersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Teacher[]> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = _dbContext.Set<Teacher>().Where(w =>
                (filter.CafedraName == null || w.Cafedra.CafedraName == filter.CafedraName) &&
                (filter.DegreeName == null || w.Degree.DegreeName == filter.DegreeName) &&
                (filter.PositionName == null || w.Position.PositionName == filter.PositionName))
                .ToArrayAsync(cancellationToken);
            return teachers;
        }

    }

}
