using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class DepartmetSubjectConfig : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(ds => new { ds.DID, ds.SubID });
            builder.HasOne(ds => ds.Department)
            .WithMany(d => d.DepartmentSubjects)
            .HasForeignKey(ds => ds.DID);

            builder.HasOne(ds => ds.Subject)
            .WithMany(s => s.DepartmetsSubjects)
            .HasForeignKey(ds => ds.SubID);
        }
    }
}
