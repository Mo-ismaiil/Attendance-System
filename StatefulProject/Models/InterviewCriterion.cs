﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StatefulProject
{
    public partial class InterviewCriterion
    {
        public InterviewCriterion()
        {
            DeptCriteria = new HashSet<DeptCriterion>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("CriteriaNavigation")]
        public virtual ICollection<DeptCriterion> DeptCriteria { get; set; }
    }
}