using Microsoft.EntityFrameworkCore;

namespace StatefulProject.Data
{
    public class StudentConc:IStudent
    {
        private ApplicationDbContext context { get; set; }
        public StudentConc(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        //Implement IStudent Methods.
        public IEnumerable<Student> getAbsentStudents(int deptID, DateTime date)
        {
            var attendedStudentsToday = context.Attendances
                .Where(a => a.AttendanceDate == date)
                .Select(s=> s.StudentId);
            return context.Students.Include(s=>s.User).Where(student => attendedStudentsToday.Contains(student.StudentId) == false && student.DepartmentId == deptID && student.User!= null);
        }
       
        public IEnumerable<Student> GetAttendedStudents(int deptID, DateTime date)
        {
            var attendedStudentsToday = context.Attendances
                .Where(a => a.AttendanceDate == date)
                .Select(s => s.StudentId);
            return context.Students.Include(s => s.User).Where(student => attendedStudentsToday.Contains(student.StudentId) == true && student.DepartmentId == deptID && student.User != null);
        }

        public void addAttendedStudents(IEnumerable<int> studentsIDs, DateTime date)
        {
            TimeSpan arrivalTime = DateTime.Now.TimeOfDay;
            foreach (var id in studentsIDs)
            {
                context.Attendances.Add(new Attendance() { StudentId=id, AttendanceDate = date, ArrivalTime = arrivalTime});
                context.SaveChanges();
            }
        }

        public void undoAttendedStudents(IEnumerable<int> studentsIDs, DateTime date)
        {
            TimeSpan arrivalTime = DateTime.Now.TimeOfDay;
            foreach (var id in studentsIDs)
            {
                context.Attendances.Remove(new Attendance() { StudentId = id, AttendanceDate = date });
                context.SaveChanges();
            }
        }

        public Student GetStudentByID(int id)
        {
            throw new NotImplementedException();
        }
        /////////////////////sokary

        //get all students
        public List<Student> GetStudents()
        {
            return context.Students.Include(s=>s.User).ToList();
        }
        //find student by id
        public Student FindStudentById(int stdId)
        {
            var std = context.Students.Include(s => s.User).FirstOrDefault(a => a.StudentId == stdId);
            return std;
        }
        //find dtudent by the department
        public List<Student> FinfStudentsByDepartment(int dep)
        {
            var std = context.Students.Include(s => s.User).ToList().FindAll(a => a.DepartmentId == dep);
            return std;
        }
        //add studnet
        public void AddStudent(ApplicationUser User)
        {
            context.Add(new Student()
            {
                Id = User.Id,
                User = User
            });
            context.SaveChanges();
        }
        //update student
        public void UpdateStudent(int id, Student s)
        {
            //var OldStudent = FindStudentById(id);
            var OldStudent = context.Students.Include(s => s.User).Where(x => x.StudentId == id && x.User.Id==x.Id).SingleOrDefault();
            // OldStudent = s;
           
            OldStudent.User.FullNameEn = s.User.FullNameEn;
            OldStudent.User.FullNameAr = s.User.FullNameAr;
            OldStudent.DepartmentId = s.DepartmentId;
            OldStudent.User.Gender = s.User.Gender;
            OldStudent.User.BirthDate = s.User.BirthDate;
            OldStudent.Address = s.Address;
            OldStudent.Code = s.Code;
            OldStudent.Faculty = s.Faculty;
            OldStudent.GraduationGrade = s.GraduationGrade;
            OldStudent.HomePhone = s.HomePhone;
            OldStudent.Mobile = s.Mobile;
            OldStudent.Specialization = s.Specialization;
            OldStudent.StudentStatus = s.StudentStatus;
            OldStudent.University = s.University;

            context.SaveChanges();

        }

        public void UpdateWarning(int id, int warning)
        {
            //var OldStudent = FindStudentById(id);
            //OldStudent.warning = warning;

        }
               
    }
}
