﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StatefulProject
{
    [Table("Interviewee")]
    [Index("DeptId", Name = "IX_Interviewee_DeptId")]
    [Index("InstructorId", Name = "IX_Interviewee_InstructorId")]
    public partial class Interviewee
    {
        public Interviewee()
        {
            InterviewResults = new HashSet<InterviewResult>();
        }

        [Key]
        public int Id { get; set; }
        public string InstructorId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string IdNum { get; set; }
        public int Ser { get; set; }
        public DateTime? IntDate { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public string GraduationGrade { get; set; }
        public string Mobile { get; set; }
        public string HomePhone { get; set; }
        public string MillarityStatus { get; set; }
        public string Email { get; set; }
        [Column("attend")]
        public bool? Attend { get; set; }
        public bool AcceptedTechnical { get; set; }
        public int TechTotal { get; set; }
        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        [InverseProperty("Interviewees")]
        public virtual InterviewDept Dept { get; set; }
        [InverseProperty("Interviewee")]
        public virtual ICollection<InterviewResult> InterviewResults { get; set; }
    }
}