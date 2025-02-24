using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database.Helpers;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Database.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        private const string TableName = "Teacher";
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(p => p.TeacherId).HasName($"pk_{TableName}_teacher_id");
            builder.Property(p => p.TeacherId).ValueGeneratedOnAdd().HasColumnName("Teacher_ID");

            builder.Property(p => p.Surname)
                .IsRequired()
                .HasColumnName("Surname")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);
            builder.Property(p => p.Patronym)
                .IsRequired()
                .HasColumnName("Patronym")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);



            //TODO: связи9
            builder.Property(p => p.CafedraId)
                .IsRequired()
                .HasColumnName("Cafedra_ID")
                .HasColumnType(ColumnType.Int);

            


        }
    }
}