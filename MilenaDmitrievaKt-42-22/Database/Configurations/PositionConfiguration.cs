using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilenaDmitrievaKt_42_22.Database.Helpers;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Database.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        private const string TableName = "Position";
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.PositionId).HasName($"pk_{TableName}_position_id");
            builder.Property(p => p.PositionId).ValueGeneratedOnAdd().HasColumnName("Position_ID");

            builder.Property(p => p.PositionName)
                .IsRequired()
                .HasColumnName("Position_Name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);
           
        }
    }
}