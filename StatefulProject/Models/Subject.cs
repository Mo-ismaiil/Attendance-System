﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StatefulProject
{
    [Index("DepartmentId", Name = "IX_Subjects_DepartmentId")]
    public partial class Subject
    {
        public Subject()
        {
            Exams = new HashSet<Exam>();
            Questions = new HashSet<Question>();
        }

        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public int LectHours { get; set; }
        public int LabHours { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Subjects")]
        public virtual Department Department { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}