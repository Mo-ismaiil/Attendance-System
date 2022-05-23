namespace StatefulProject.Data
{
    public interface StudentInterface
    {
        public List<Student> GetStudents();
        public Student FindStudentById(int stdId);
        public List<Student> FinfStudentsByDepartment(int dep);
        public void AddStudent(Student NewStudent);
        public void UpdateStudent(int id, Student s);
        public void UpdateWarning(int id, int warning);

    }
}
