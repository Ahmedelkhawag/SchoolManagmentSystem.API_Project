﻿using SchoolManagmentSystem.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities
{
    public class Student : GeneralLocalizableEntities
    {

        [Key]
        public int StudID { get; set; }
        [StringLength(200)]
        public string NameAr { get; set; }
        [StringLength(200)]
        public string NameEn { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(500)]
        public string Phone { get; set; }
        public int? DID { get; set; }

        [ForeignKey("DID")]
        public virtual Department Department { get; set; }
    }
}
