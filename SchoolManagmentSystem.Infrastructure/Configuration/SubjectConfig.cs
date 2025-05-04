using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class SubjectConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(s => s.SubjectNameEn).HasMaxLength(150).HasColumnType("varchar");
            builder.Property(s => s.SubjectNameAr).HasMaxLength(150).HasColumnType("varchar");
            builder.HasIndex(s => s.Period)
             .IsUnique(false);
        }
    }
}
