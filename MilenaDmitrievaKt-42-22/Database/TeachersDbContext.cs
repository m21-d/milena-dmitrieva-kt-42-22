using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Models;
using MilenaDmitrievaKt_42_22.Database.Configurations;
namespace MilenaDmitrievaKt_42_22.Database
{
    public class TeachersDbContext : DbContext
    {
        DbSet<Cafedra> Cafedra { get; set; }
        DbSet<Degree> Degree { get; set; }
        DbSet<Position> Position { get; set; }
        DbSet<Teacher> Teacher { get; set; }
        DbSet<Subject> Subject { get; set; }
        DbSet<Lessons> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CafedraConfiguration());
            modelBuilder.ApplyConfiguration(new DegreeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new LessonsConfiguration());
        }
        public TeachersDbContext(DbContextOptions<TeachersDbContext> options) : base(options)
        {
        }
    }
}
