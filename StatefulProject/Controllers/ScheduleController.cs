using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StatefulProject.Data;
using StatefulProject.Models.ScheduleViewModel;

namespace StatefulProject.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        ScheduleViewModel Vmodel;
        public ISchedule ContextSch;
        public IDepartment ContextDept;
        private readonly ILogger<ScheduleController> Logger;

        public ScheduleController(ISchedule _ContextSch, ILogger<ScheduleController> _Logger, IDepartment _ContextDept)
        {
            Vmodel = new ScheduleViewModel();
            ContextSch = _ContextSch;
            ContextDept = _ContextDept;
            Logger = _Logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Vmodel.Departments = new SelectList(ContextDept.getDepartments(), "Id", "FullName");
            Vmodel.HttpMethod = "GET";
            return View(Vmodel);
        }

        [HttpPost]
        public IActionResult Index(ScheduleViewModel scheduleViewModel)
        {
            ModelState.Remove(nameof(ScheduleViewModel.Departments));
            ModelState.Remove(nameof(ScheduleViewModel.GetSchedules));
            ModelState.Remove(nameof(ScheduleViewModel.HttpMethod));

            scheduleViewModel.Departments = new SelectList(ContextDept.getDepartments(), "Id", "FullName", scheduleViewModel.selectedDeptID);

            if (ModelState.IsValid)
            {
                int TotalDays = (scheduleViewModel.DateTo - scheduleViewModel.DateFrom).Days + 1;
                if (TotalDays <= 30)
                {
                    Department Department = ContextDept.getDepartmentById(scheduleViewModel.selectedDeptID);
                    scheduleViewModel.HttpMethod = HttpContext.Request.Method;
                    scheduleViewModel.GetSchedules = ContextSch.GetSchedules(Department.Id, scheduleViewModel.DateFrom, scheduleViewModel.DateTo);
                    DateTime LastDate = scheduleViewModel.DateFrom;
                    if (scheduleViewModel.GetSchedules.Count == 0)
                    {
                        for(int i=0; i<TotalDays; i++)
                        {
                            scheduleViewModel.GetSchedules.Add(new Schedule() { DepartmentId = Department.Id, Department = Department, LectPeriod = 1, ScheduleDate = LastDate });
                            LastDate = LastDate.AddDays(1);
                        }
                    }
                    else
                    {
                        int iterator = 0;
                        while(iterator < TotalDays)
                        {
                            if(iterator == scheduleViewModel.GetSchedules.Count)
                            {
                                scheduleViewModel.GetSchedules.Add(new Schedule() { DepartmentId = Department.Id, Department = Department, LectPeriod = 1, ScheduleDate = LastDate });
                            }
                            else if(scheduleViewModel.GetSchedules[iterator].ScheduleDate > LastDate)
                            {
                                scheduleViewModel.GetSchedules.Insert(iterator, new Schedule() { DepartmentId = Department.Id, Department = Department, LectPeriod = 1, ScheduleDate = LastDate });
                            }
                            ++iterator;
                            LastDate = LastDate.AddDays(1);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Exceeded Amount", "Exceeded 30 Days!");
                }
            }
            return View(scheduleViewModel);
        }

        [HttpPost]
        public IActionResult SetSchedule(List<Schedule> Schedules, int DepartmentId)
        {
            Department Department = ContextDept.getDepartmentById(DepartmentId);
            if (ModelState.IsValid)
            {
                ContextSch.SetSchedules(Schedules, Department);
            }
            else
            {
                ModelState.AddModelError("SchedulesError", "Invalid Schedules Submitted");
            }
            return RedirectToAction("Index");
        }
    }
}
