using Microsoft.AspNetCore.Mvc.Rendering;
using StatefulProject;

namespace attendanceSystemStatefulProject.Models.attendanceViewModel
{
    public class attendanceViewModel
    {
        public IEnumerable<Student> Attendedstudents { get; set; }
        public IEnumerable<Student> Absentstudents { get; set; }
        public SelectList Departments { get; set; }
        public int selectedDeptID { get; set; }
        public DateTime date { get; set; }
    }
}
