using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database.Helpers;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Database.Configurations
{
    public class LessonsConfiguration : IEntityTypeConfiguration<Lessons>
    {
        private const string TableName = "Lessons";
        public void Configure(EntityTypeBuilder<Lessons> builder)
        {
            builder.HasKey(p => p.LessonsId).HasName($"pk_{TableName}_lessons_id");
            builder.Property(p => p.LessonsId).ValueGeneratedOnAdd().HasColumnName("Lessons_ID");

            builder.Property(p => p.Hours)
                .IsRequired()
                .HasColumnName("Hours")
                .HasColumnType(ColumnType.Int);
            
            ////
            builder.Property(p => p.TeacherId)
                .IsRequired()
                .HasColumnName("Teacher_ID")
                .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Teacher)
                .WithMany()
                .HasForeignKey(p => p.TeacherId)
                .HasConstraintName("fk_teacher_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.TeacherId, $"idx_{TableName}_fk_teacher_id");
            builder.Navigation(p => p.Teacher)
                .AutoInclude();



            ////
            builder.Property(p => p.SubjectId)
               .IsRequired()
               .HasColumnName("Subject_ID")
               .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Subject)
                .WithMany()
                .HasForeignKey(p => p.SubjectId)
                .HasConstraintName("fk_subject_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.SubjectId, $"idx_{TableName}_fk_subject_id");
            builder.Navigation(p => p.Subject)
                .AutoInclude();

        }
    }
}
