using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Interfaces
{
    public interface ICafedraService
    {
        public Task<Cafedra[]> GetCafedrasAsync(CafedraFilter filter, CancellationToken cancellationToken);
        public Task<int> DeleteCafedraAsync(int id, CancellationToken cancellationToken);
        public Task<int> UpdateCafedraAsync(CafedraUpdate newCafedra, CancellationToken cancellationToken);
        public Task<int> AddCafedraAsync(CafedraAdd newCafedra, CancellationToken cancellationToken);
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
                (filter.NumberOfTeachers == null || _dbContext.Set<Teacher>().Where(e => e.CafedraId == w.CafedraId).Count() == filter.NumberOfTeachers)).Include("Head").Select(t => new Cafedra

                {
                    CafedraId = t.CafedraId,
                    CafedraName = t.CafedraName,
                    Year = t.Year,
                    HeadId = t.HeadId,
                    Head = t.Head == null ? null : new Teacher
                    {
                        TeacherId = t.Head.TeacherId,
                        Surname = t.Head.Surname,
                        Name = t.Head.Name,
                        Patronym = t.Head.Patronym,
                        CafedraId = t.Head.CafedraId,
                        DegreeId = t.Head.DegreeId,
                        Degree = t.Head.Degree,
                        PositionId = t.Head.PositionId,
                        Position = t.Head.Position
                    }
                })
                .ToArrayAsync(cancellationToken);
            return a;
        }
        public Task<int> DeleteCafedraAsync(int id, CancellationToken cancellationToken = default)
        {
            
            var t = _dbContext.Set<Cafedra>().Find(id);
            if (t != null)
                _dbContext.Set<Cafedra>().Remove(t);
            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
        public Task<int> UpdateCafedraAsync(CafedraUpdate newCafedra, CancellationToken cancellationToken = default)
        {
            var b = _dbContext.Set<Cafedra>().Where(e => e.CafedraId == newCafedra.CafedraId);//TODO проверка на существование и вывод ошибки?
            if (b.Any())
            {
                var a = b.First();
                if (newCafedra.CafedraName != null)
                    a.CafedraName = newCafedra.CafedraName;
                if (newCafedra.Year != null)
                    a.Year = (int)newCafedra.Year;
                if (newCafedra.HeadId != null)
                    a.HeadId = newCafedra.HeadId;
            }

            var a1 = _dbContext.SaveChangesAsync(cancellationToken);
            return a1;
        }
        public Task<int> AddCafedraAsync(CafedraAdd newCafedra, CancellationToken cancellationToken = default)
        {

            Cafedra cafedra = new Cafedra
            {
                CafedraName = newCafedra.CafedraName,
                Year = newCafedra.Year
            };

            if (newCafedra.HeadId != null)
                cafedra.HeadId = newCafedra.HeadId;

            _dbContext.Set<Cafedra>().Add(cafedra);


            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
    }
}
