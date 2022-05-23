namespace StatefulProject.Data
{
    public interface IStudent
    {
        //Add student DB method signatures.
        IEnumerable<Student> getAbsentStudents(int deptID, DateTime date);
        IEnumerable<Student> GetAttendedStudents(int deptID, DateTime date);
        Student GetStudentByID(int id);
        void addAttendedStudents(IEnumerable<int> students, DateTime date);
        void undoAttendedStudents(IEnumerable<int> students, DateTime date);
        /////sokary
        public List<Student> GetStudents();
        public Student FindStudentById(int stdId);
        public List<Student> FinfStudentsByDepartment(int dep);
        public void AddStudent(ApplicationUser NewStudent);
        public void UpdateStudent(int id, Student s);
        public void UpdateWarning(int id, int warning);
    }

}
