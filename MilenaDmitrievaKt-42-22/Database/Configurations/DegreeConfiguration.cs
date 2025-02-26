using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database.Helpers;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Database.Configurations
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        private const string TableName = "Degree";
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.HasKey(p => p.DegreeId).HasName($"pk_{TableName}_degree_id");
            builder.Property(p => p.DegreeId).ValueGeneratedOnAdd().HasColumnName("Degree_ID");

            builder.Property(p => p.DegreeName)
                .IsRequired()
                .HasColumnName("Degree_Name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);

        }
    }
}