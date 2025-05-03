using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Ins_Subject> Ins_Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudID, ss.SubID });

            modelBuilder.Entity<DepartmetSubject>()
                .HasKey(ds => new { ds.DID, ds.SubID });
            modelBuilder.Entity<Ins_Subject>()
                .HasKey(IS => new { IS.InsId, IS.SubID });

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.InsManagerId)
                .IsUnique(false);

            base.OnModelCreating(modelBuilder);
        }




    }
}
