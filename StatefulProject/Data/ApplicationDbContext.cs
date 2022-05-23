﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StatefulProject.Data
{
    //DBContext with ApplicationUser instead of default IdentityUser.
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<BuildingAffairsAttendance> BuildingAffairsAttendances { get; set; }
        public virtual DbSet<BuildingAffairsStaff> BuildingAffairsStaffs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompnayJobProfile> CompnayJobProfiles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DeptCriterion> DeptCriteria { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<GraduatedDepartment> GraduatedDepartments { get; set; }
        public virtual DbSet<GraduatedIntake> GraduatedIntakes { get; set; }
        public virtual DbSet<GraduatedStudent> GraduatedStudents { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<InterviewCriterion> InterviewCriteria { get; set; }
        public virtual DbSet<InterviewDept> InterviewDepts { get; set; }
        public virtual DbSet<InterviewResult> InterviewResults { get; set; }
        public virtual DbSet<Interviewee> Interviewees { get; set; }
        public virtual DbSet<Itiprogram> Itiprograms { get; set; }
        public virtual DbSet<JobProfile> JobProfiles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<StudentCompany> StudentCompanies { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<StudentPermission> StudentPermissions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Invoke base OnModelCreating for ApplicationUser (Identity) constraints building.
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.AttendanceDate });
            });

            modelBuilder.Entity<BuildingAffairsAttendance>(entity =>
            {
                entity.HasKey(e => new { e.StaffId, e.AttendanceDate, e.ArrivalTime });
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.StudentId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<GraduatedStudent>(entity =>
            {
                entity.HasOne(d => d.Intake)
                    .WithMany(p => p.GraduatedStudents)
                    .HasForeignKey(d => d.IntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Instructors_Id")
                    .IsUnique()
                    .HasFilter("([Id] IS NOT NULL)");
            });

            modelBuilder.Entity<InterviewResult>(entity =>
            {
                entity.HasOne(d => d.DeptCriteria)
                    .WithMany(p => p.InterviewResults)
                    .HasForeignKey(d => d.DeptCriteriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Students_Id")
                    .IsUnique()
                    .HasFilter("([Id] IS NOT NULL)");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}