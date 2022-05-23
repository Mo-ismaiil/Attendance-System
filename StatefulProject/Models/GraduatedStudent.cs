﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StatefulProject
{
    [Index("DepartmentId", Name = "IX_GraduatedStudents_DepartmentId")]
    [Index("IntakeId", Name = "IX_GraduatedStudents_IntakeId")]
    public partial class GraduatedStudent
    {
        public GraduatedStudent()
        {
            StudentCompanies = new HashSet<StudentCompany>();
        }

        [Key]
        public int Id { get; set; }
        public int IntakeId { get; set; }
        public int DepartmentId { get; set; }
        public string GradutedId { get; set; }
        public string GradutedNameAr { get; set; }
        public string GradutedNameEn { get; set; }
        public int GradutedGender { get; set; }
        public DateTime BirthDate { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specializations { get; set; }
        public int GraduationYear { get; set; }
        public string GraduationGrade { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string HomePhone { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("GraduatedStudents")]
        public virtual GraduatedDepartment Department { get; set; }
        [ForeignKey("IntakeId")]
        [InverseProperty("GraduatedStudents")]
        public virtual GraduatedIntake Intake { get; set; }
        [InverseProperty("GraduatedStudents")]
        public virtual ICollection<StudentCompany> StudentCompanies { get; set; }
    }
}