using SchoolManagmentSystem.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities
{
    public class DepartmetSubject : GeneralLocalizableEntities
    {
        //[Key]
        //public int DeptSubID { get; set; }
        [Key]
        public int DID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty(nameof(Department.DepartmentSubjects))]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty(nameof(Subject.DepartmetsSubjects))]
        public virtual Subject? Subject { get; set; }
    }
}
