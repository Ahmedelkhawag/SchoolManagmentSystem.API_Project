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
            modelBuilder.HasDefaultSchema("School");

            #region fluent api  Config

            //modelBuilder.Entity<StudentSubject>(entity =>
            //{

            //    entity.HasKey(ss => new { ss.StudID, ss.SubID });
            //    entity.HasOne(ss => ss.Student)
            //    .WithMany(s => s.StudentSubjects)
            //    .HasForeignKey(ss => ss.StudID);
            //    entity.HasOne(ss => ss.Subject)
            //    .WithMany(s => s.StudentsSubjects)
            //    .HasForeignKey(ss => ss.SubID);

            //});


            //modelBuilder.Entity<DepartmetSubject>(entity =>
            //{
            //    entity.HasKey(ds => new { ds.DID, ds.SubID });
            //    entity.HasOne(ds => ds.Department)
            //    .WithMany(d => d.DepartmentSubjects)
            //    .HasForeignKey(ds => ds.DID);

            //    entity.HasOne(ds => ds.Subject)
            //    .WithMany(s => s.DepartmetsSubjects)
            //    .HasForeignKey(ds => ds.SubID);

            //});

            //modelBuilder.Entity<Ins_Subject>(entity =>
            //{

            //    entity.HasKey(i => new { i.InsId, i.SubID });
            //    entity.HasOne(i => i.Instructor)
            //    .WithMany(ins => ins.Ins_Subjects)
            //    .HasForeignKey(i => i.InsId);

            //    entity.HasOne(i => i.Subject)
            //    .WithMany(s => s.Ins_Subjects)
            //    .HasForeignKey(i => i.SubID);
            //});



            //modelBuilder.Entity<Department>(entity =>
            //{
            //    entity.Property(d => d.DNameEn).HasMaxLength(150).HasColumnType("varchar");
            //    entity.Property(d => d.DNameAr).HasMaxLength(150).HasColumnType("varchar");
            //    entity.HasIndex(d => d.InsManagerId)
            //    .IsUnique(false);

            //    //entity.HasMany(d => d.Students)
            //    //.WithOne(s => s.Department)
            //    //.HasForeignKey(s => s.DID);

            //    //entity.HasOne(d => d.Instructor)
            //    //.WithOne(i => i.Department)
            //    //.HasForeignKey<Department>(i => i.InsManagerId);

            //});
            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(s => s.NameEn).HasMaxLength(200).HasColumnType("varchar");
            //    entity.Property(s => s.NameAr).HasMaxLength(200).HasColumnType("varchar");
            //    entity.Property(s => s.Address).HasMaxLength(500).HasColumnType("varchar");
            //    entity.Property(s => s.Phone).HasMaxLength(500).HasColumnType("varchar");
            //    entity.HasIndex(s => s.DID)
            //    .IsUnique(false);
            //    //entity.HasOne(s => s.Department)
            //    //.WithMany(d => d.Students)
            //    //.HasForeignKey(s => s.DID);


            //});

            //modelBuilder.Entity<Subject>(entity =>
            //{
            //    entity.Property(s => s.SubjectNameEn).HasMaxLength(150).HasColumnType("varchar");
            //    entity.Property(s => s.SubjectNameAr).HasMaxLength(150).HasColumnType("varchar");
            //    entity.HasIndex(s => s.Period)
            //    .IsUnique(false);

            //});
            //modelBuilder.Entity<Instructor>(entity =>
            //{

            //    entity.Property(i => i.ENameEn).HasMaxLength(200).HasColumnType("varchar");
            //    entity.Property(i => i.ENameAr).HasMaxLength(200).HasColumnType("varchar");
            //    entity.Property(i => i.Address).HasMaxLength(500).HasColumnType("varchar");
            //    entity.Property(i => i.Position).HasMaxLength(500).HasColumnType("varchar");
            //    entity.HasIndex(i => i.DId)
            //    .IsUnique(false);

            //    entity.HasOne(i => i.Supervisor)
            //    .WithMany(d => d.Instructors)
            //    .HasForeignKey(i => i.SupervisorId);

            //    entity.HasOne(i => i.Department)
            //    .WithOne(d => d.Instructor)
            //    .HasForeignKey<Instructor>(d => d.DId);


            //});

            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
