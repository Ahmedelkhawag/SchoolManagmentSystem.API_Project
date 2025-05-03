using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }

        public int? SupervisorId { get; set; }
        public decimal Salary { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructors))]
        public virtual Instructor? Supervisor { get; set; }
        [InverseProperty(nameof(Supervisor))]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [InverseProperty(nameof(Ins_Subject.Instructor))]
        public ICollection<Ins_Subject> Ins_Subjects { get; set; }

        [ForeignKey(nameof(Department))]
        [InverseProperty(nameof(Department.Instructors))]
        public int? DId { get; set; }
        public virtual Department? Department { get; set; }

        [InverseProperty("Instructor")]
        public virtual Department? DepartmentManager { get; set; }
    }
}
