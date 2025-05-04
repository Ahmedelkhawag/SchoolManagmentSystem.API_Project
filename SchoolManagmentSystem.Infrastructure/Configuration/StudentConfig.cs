using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.NameEn).HasMaxLength(200).HasColumnType("varchar");
            builder.Property(s => s.NameAr).HasMaxLength(200).HasColumnType("varchar");
            builder.Property(s => s.Address).HasMaxLength(500).HasColumnType("varchar");
            builder.Property(s => s.Phone).HasMaxLength(500).HasColumnType("varchar");
            builder.HasIndex(s => s.DID)
              .IsUnique(false);
        }
    }
}
