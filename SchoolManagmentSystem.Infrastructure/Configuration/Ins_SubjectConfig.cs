using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class Ins_SubjectConfig : IEntityTypeConfiguration<Ins_Subject>
    {
        public void Configure(EntityTypeBuilder<Ins_Subject> builder)
        {
            builder.HasKey(i => new { i.InsId, i.SubID });
            builder.HasOne(i => i.Instructor)
            .WithMany(ins => ins.Ins_Subjects)
            .HasForeignKey(i => i.InsId);

            builder.HasOne(i => i.Subject)
            .WithMany(s => s.Ins_Subjects)
            .HasForeignKey(i => i.SubID);
        }
    }
}
