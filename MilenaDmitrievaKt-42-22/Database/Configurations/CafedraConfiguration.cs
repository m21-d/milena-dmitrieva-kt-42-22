using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilenaDmitrievaKt_42_22.Database.Helpers;
using MilenaDmitrievaKt_42_22.Models;

namespace MilenaDmitrievaKt_42_22.Database.Configurations
{
    public class CafedraConfiguration : IEntityTypeConfiguration<Cafedra>
    {
        private const string TableName = "Cafedra";
        public void Configure(EntityTypeBuilder<Cafedra> builder)
        {
            builder.HasKey(p => p.CafedraId).HasName($"pk_{TableName}_cafedra_id");
            builder.Property(p => p.CafedraId).ValueGeneratedOnAdd().HasColumnName("Cafedra_ID");

            builder.Property(p => p.CafedraName)
                .IsRequired()
                .HasColumnName("Cafedra_Name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);
            builder.Property(p => p.Year)
                .IsRequired()
                .HasColumnName("Year")
                .HasColumnType(ColumnType.Int);
            builder.Property(p => p.Head)
                .IsRequired()
                .HasColumnName("Head_ID")
                .HasColumnType(ColumnType.Int);

            //TODO: связи9

            
        }
    }
}
