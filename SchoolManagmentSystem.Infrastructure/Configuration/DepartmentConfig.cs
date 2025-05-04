using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.DNameEn).HasMaxLength(150).HasColumnType("varchar");
            builder.Property(d => d.DNameAr).HasMaxLength(150).HasColumnType("varchar");
            builder.HasIndex(d => d.InsManagerId)
            .IsUnique(false);
        }
    }
}
