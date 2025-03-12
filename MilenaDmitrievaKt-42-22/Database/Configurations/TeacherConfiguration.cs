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


            ////
            builder.Property(p => p.CafedraId)
                .IsRequired()
                .HasColumnName("Cafedra_ID")
                .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Cafedra)
                .WithMany()
                .HasForeignKey(p => p.CafedraId)
                .HasConstraintName("fk_cafedra_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.CafedraId, $"idx_{TableName}_fk_cafedra_id");
            //builder.Navigation(p => p.Cafedra).AutoInclude();



            ////
            builder.Property(p => p.DegreeId)
               .IsRequired()
               .HasColumnName("Degree_ID")
               .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Degree)
                .WithMany()
                .HasForeignKey(p => p.DegreeId)
                .HasConstraintName("fk_degree_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.DegreeId, $"idx_{TableName}_fk_degree_id");
            builder.Navigation(p => p.Degree)
                .AutoInclude();

            ////
            builder.Property(p => p.PositionId)
               .IsRequired()
               .HasColumnName("Position_ID")
               .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Position)
                .WithMany()
                .HasForeignKey(p => p.PositionId)
                .HasConstraintName("fk_position_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.PositionId, $"idx_{TableName}_fk_position_id");
            builder.Navigation(p => p.Position)
                .AutoInclude();
        }
    }
}