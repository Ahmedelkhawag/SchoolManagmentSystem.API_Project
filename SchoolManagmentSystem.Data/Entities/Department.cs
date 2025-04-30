using SchoolManagmentSystem.Data.Commons;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem.Data.Entities
{
    public partial class Department : GeneralLocalizableEntities
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
        }
        [Key]
        public int DID { get; set; }
        [StringLength(150)]
        public string DNameAr { get; set; }
        [StringLength(150)]
        public string DNameEn { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
    }
}
