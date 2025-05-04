using MilenaDmitrievaKt_42_22.Database;
using MilenaDmitrievaKt_42_22.Filters.TeacherFilters;
using MilenaDmitrievaKt_42_22.Models;
using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Filters;

namespace MilenaDmitrievaKt_42_22.Interfaces.TeachersInterfaces
{
    public interface ITeacherService
    {
        public Task<Teacher[]> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken);
        public Task<int> DeleteTeacherAsync(int id, CancellationToken cancellationToken);
        public Task<int> UpdateTeacherAsync(TeacherUpdate t, CancellationToken cancellationToken);
        public Task<int> AddTeacherAsync(TeacherAdd t, CancellationToken cancellationToken);
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
                (filter.PositionName == null || w.Position.PositionName == filter.PositionName)).Include("Cafedra.Head").Select(t => new Teacher
                {
                    TeacherId = t.TeacherId,
                    Surname = t.Surname,
                    Name = t.Name,
                    Patronym = t.Patronym,
                    CafedraId = t.CafedraId,
                    Cafedra = new Cafedra
                    {
                        CafedraId = t.Cafedra.CafedraId,
                        CafedraName = t.Cafedra.CafedraName,
                        Year = t.Cafedra.Year,
                        HeadId = t.Cafedra.HeadId,
                        Head = t.Cafedra.Head == null ? null : new Teacher
                        {
                            TeacherId = t.Cafedra.Head.TeacherId,
                            Surname = t.Cafedra.Head.Surname,
                            Name = t.Cafedra.Head.Name,
                            Patronym = t.Cafedra.Head.Patronym,
                            CafedraId = t.Cafedra.Head.CafedraId,
                            DegreeId = t.Cafedra.Head.DegreeId,
                            Degree = t.Cafedra.Head.Degree,
                            PositionId = t.Cafedra.Head.PositionId,
                            Position = t.Cafedra.Head.Position
                        }
                    },
                    DegreeId = t.DegreeId,
                    Degree = t.Degree,
                    PositionId = t.PositionId,
                    Position = t.Position
                })
                .ToArrayAsync(cancellationToken);
            return teachers;
        }
        public Task<int> DeleteTeacherAsync(int id, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<Cafedra>().Where(e => e.HeadId == id).ExecuteUpdate(e => e.SetProperty(t => t.HeadId, t1=>null));
            var a = _dbContext.Set<Teacher>().Where(e => e.TeacherId == id).ExecuteDeleteAsync(cancellationToken);

            return a;
        }
        public Task<int> UpdateTeacherAsync(TeacherUpdate t, CancellationToken cancellationToken = default)
        {
            var b = _dbContext.Set<Teacher>().Where(e => e.TeacherId == t.TeacherId);
            if (b.Any())
            {
                var a = b.First();
                if (t.Surname != null)
                    a.Surname = t.Surname;
                if (t.Name != null)
                    a.Name = t.Name;
                if (t.Patronym != null)
                    a.Patronym = t.Patronym;
                if (t.CafedraId != null)
                    a.CafedraId = (int)t.CafedraId;
                if (t.DegreeId != null)
                    a.DegreeId = (int)t.DegreeId;
                if (t.PositionId != null)
                    a.PositionId = (int)t.PositionId;
            }

            var a1 = _dbContext.SaveChangesAsync(cancellationToken);
            return a1;
        }
        public Task<int> AddTeacherAsync(TeacherAdd t, CancellationToken cancellationToken = default)
        {

            Teacher t1 = new Teacher
            {
                Surname = t.Surname,
                Name = t.Name,
                Patronym = t.Patronym,
                CafedraId = t.CafedraId,
                DegreeId = t.DegreeId,
                PositionId = t.PositionId
            };

            _dbContext.Set<Teacher>().Add(t1);

            var a = _dbContext.SaveChangesAsync(cancellationToken);
            return a;
        }
    }

}
