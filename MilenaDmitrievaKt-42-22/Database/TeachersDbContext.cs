using Microsoft.EntityFrameworkCore;
namespace MilenaDmitrievaKt_42_22.Database
{
    public class TeachersDbContext : DbContext
    {
        public TeachersDbContext(DbContextOptions<TeachersDbContext> options) : base(options)
        {
        }
    }
}
