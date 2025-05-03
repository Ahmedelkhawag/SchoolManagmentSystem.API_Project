using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities
{
    public class Ins_Subject
    {
        [Key]
        public int InsId { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey(nameof(InsId))]
        [InverseProperty(nameof(Instructor.Ins_Subjects))]
        public Instructor? Instructor { get; set; }

        [ForeignKey(nameof(SubID))]
        [InverseProperty(nameof(Subject.Ins_Subjects))]
        public Subject? Subject { get; set; }
    }
}
