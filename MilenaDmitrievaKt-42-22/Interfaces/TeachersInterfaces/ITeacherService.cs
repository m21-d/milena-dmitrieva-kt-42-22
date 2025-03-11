using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters.TeacherFilters;
using MilenaDmitrievaKt_42_22.Models;
using Microsoft.EntityFrameworkCore;
namespace MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces
{
    public interface ITeacherService
    {
        public Task<Teacher[]> GetTeachersByCafedraAsync(TeacherCafedraFilter filter, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeachersDbContext _dbContext;
        public TeacherService(TeachersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Teacher[]>GetTeachersByCafedraAsync(TeacherCafedraFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers=_dbContext.Set<Teacher>().Where(w=>w.Cafedra.CafedraName==filter.CafedraName).ToArrayAsync(cancellationToken);
            return teachers;
        }
    }

}
