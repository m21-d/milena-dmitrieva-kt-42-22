using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ICafedraService
    {
        public Task<Cafedra[]> GetCafedrasAsync(CafedraFilter filter, CancellationToken cancellationToken);
    }

    public class CafedraService : ICafedraService
    {
        private readonly TeachersDbContext _dbContext;
        public CafedraService(TeachersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Cafedra[]> GetCafedrasAsync(CafedraFilter filter, CancellationToken cancellationToken = default)
        {
            var a = _dbContext.Set<Cafedra>().Where(w =>
                (filter.Year == null || w.Year == filter.Year) &&
                (filter.NumberOfTeachers == null || _dbContext.Set<Teacher>().Where(e=>e.CafedraId==w.CafedraId).Count() == filter.NumberOfTeachers))
                .ToArrayAsync(cancellationToken);
            return a;
        }

    }
}
