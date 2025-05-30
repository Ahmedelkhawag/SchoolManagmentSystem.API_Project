﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Configuration
{
    public class StudentSubjectConfig : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(ss => new { ss.StudID, ss.SubID });
            builder.HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudID);
            builder.HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentsSubjects)
            .HasForeignKey(ss => ss.SubID);
        }
    }
}
