using attendanceSystemStatefulProject.Models.attendanceViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StatefulProject.Data;

namespace attendanceSystem.Controllers
{
    //[Authorize(Roles = "admin,Student,ADMIN,Admin,student")]
    [Authorize]
    public class AttendanceController : Controller
    {
        attendanceViewModel vmodel = new attendanceViewModel();

        IStudent StudentConc;
        IDepartment DepartmentConc;
        public AttendanceController(IStudent _StudentConc, IDepartment _DepartmentConc)
        {
            StudentConc = _StudentConc;
            DepartmentConc = _DepartmentConc;
        }

        public IActionResult Index(DateTime date, int selectedDeptID, bool saved)
        {
            //get the absent students of the selected dept
            if (selectedDeptID != 0 || saved == true)
            {
                vmodel.Absentstudents = StudentConc.getAbsentStudents(selectedDeptID, date);
                vmodel.Attendedstudents = StudentConc.GetAttendedStudents(selectedDeptID, date);
                vmodel.date = date;
            }
            else
            {
                //assign the date
                vmodel.date = DateTime.Today;
            }
            //get the departments for the drop down
            var departments = DepartmentConc.getDepartments();
            //assign the drop down list
            vmodel.Departments = new SelectList(departments, "Id", "ShortName");
            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Save(int[] studentsIDs)
        {
            StudentConc.addAttendedStudents(studentsIDs, DateTime.Today);
            RedirectToRoute(nameof(Index), new { saved = true });
            return Ok();
        }

        [HttpPost]
        public IActionResult Undo(int[] studentsIDs)
        {
            StudentConc.undoAttendedStudents(studentsIDs, DateTime.Today);
            RedirectToRoute(nameof(Index), new { saved = true });
            return Ok();
        }
    }
}
