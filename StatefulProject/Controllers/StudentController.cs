using Microsoft.AspNetCore.Mvc;
using attendanceSystemStatefulProject.Models.attendanceViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using StatefulProject.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace StatefulProject.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IStudent std;
        IDepartment dep;
        IStudentPermission stdPer;
        IDocument doc;
        public StudentController(IStudent _std, IDepartment _dep, IStudentPermission _stdPer, IDocument _doc) // Add 
        {
            std = _std;
            dep = _dep;
            stdPer = _stdPer;
            doc = _doc;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowStudents(int selectedDepartment)
        {

            //for the dropdown list
            List<Department> departments = dep.GetDepartment();
            ViewBag.departmentList = new SelectList(departments, "Id", "ShortName");

            List<Student> students = std.GetStudents();

            if (selectedDepartment == 0)
            {
                students = std.GetStudents();
                ViewBag.departmentName = "select department";


            }
            else
            {
                //works on the id of the selected list not right

                students = std.FinfStudentsByDepartment(selectedDepartment);
                //to make the ShortName of the department u choose appear
                ViewBag.departmentName = dep.getDepartmentById(selectedDepartment).ShortName;
            }


            return View(students);
        }

        //excel documnet
        public void ExportToExcel(int x)
        {

            //StringWriter str = new StringWriter();
            ////str.WriteLine("\*Id\*,\*Birthdate\*,\*Gender\*,ShortNameInArabic="تتتت" ,ShortNameInEnglish="AHMED",DepartmentId=5,StudentStatus="accepted",address="sidi - gaber",faculty="enginnering",universty="alexandria",graduationGrade="excellent",code=1234,homePhone=12345678,mobile=01113311203,specialization="Accounting",warning=0")

            //str.WriteLine("Id");
            //Response.Clear();
            //Response.AddHeader("content-dispotation", "Attachment; filenane = ExportedClientList.csv");
        }
        public IActionResult addStudent()
        {
            List<Department> departments = dep.GetDepartment();
            ViewBag.departmentList = new SelectList(departments, "Id", "ShortName");
            return View();
        }
        [HttpPost]
        public IActionResult addStudent(Student student)
        {
            //std.AddStudent(student);
            return RedirectToAction("ShowStudents");
        }

        //student details
        public IActionResult studentDetails(int id, int warning)
        {

            if (warning != 0)
                std.UpdateWarning(id, warning);

            Student student = std.FindStudentById(id);
            return View(student);
        }

        //update student
        public IActionResult studentUpdate(int id)
        {
            List<Department> departments = dep.GetDepartment();
            ViewBag.departmentList = new SelectList(departments, "Id", "ShortName");

            Student student = std.FindStudentById(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult studentUpdate(Student s, int id)
        {
            std.UpdateStudent(id, s);
            return RedirectToAction("ShowStudents");

        }

        //documents
        public IActionResult studentDocument(int id)
        {
            var studentDocument = doc.getDocumentByStudentId(id);
            return View(studentDocument);
        }
        [HttpPost]
        public IActionResult studentDocument(int id, Document d)
        {
            doc.updateDocument(id, d);
            return RedirectToAction("ShowStudents");
        }




        //////////////////   student permission   ///
        public IActionResult showStudentPermisssion()
        {
            List<StudentPermission> s = stdPer.getAllPermissions();
            return View(s);

        }

        //download ALL STUDENT TO excell "MUST USE PACKAGE CALLED CLOSEDXML"
        public IActionResult download()
        {
            using (var workbook = new XLWorkbook())
            {
                List<Student> StudentsList = std.GetStudents();
                var worksheet = workbook.Worksheets.Add("students");
                var currentrow = 1;
                #region Header
                worksheet.Cell(currentrow, 1).Value = "StudentId";
                worksheet.Cell(currentrow, 2).Value = "FullName EN";
                worksheet.Cell(currentrow, 3).Value = "FullName AR";
                worksheet.Cell(currentrow, 4).Value = "Addres";
                worksheet.Cell(currentrow, 5).Value = "Mobile";
                worksheet.Cell(currentrow, 6).Value = "Gender";
                #endregion

                #region Body
                foreach (var student in StudentsList)
                {
                    currentrow++;

                    worksheet.Cell(currentrow, 1).Value = student.StudentId;
                    worksheet.Cell(currentrow, 2).Value = student.User.FullNameEn;
                    worksheet.Cell(currentrow, 3).Value = student.User.FullNameAr;
                    worksheet.Cell(currentrow, 4).Value = student.Address;
                    worksheet.Cell(currentrow, 5).Value = student.Mobile;
                    worksheet.Cell(currentrow, 6).Value = student.User.Gender;




                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxnlformats-officedocument.spreadsheetml.sheet",
                        "Students.xlsx"
                        );
                }


            }
        }
    }
}
