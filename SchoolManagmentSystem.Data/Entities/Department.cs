using SchoolManagmentSystem.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities
{
    public partial class Department : GeneralLocalizableEntities
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        [StringLength(150)]
        public string? DNameAr { get; set; }
        [StringLength(150)]
        public string? DNameEn { get; set; }
        public int? InsManagerId { get; set; }
        [ForeignKey(nameof(InsManagerId))]
        [InverseProperty("DepartmentManager")]
        public virtual Instructor? Instructor { get; set; }
        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(DepartmetSubject.Department))]
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }

        [InverseProperty(nameof(Instructor.Department))]
        public virtual ICollection<Instructor> Instructors { get; set; }

    }
}
