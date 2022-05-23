using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StatefulProject.Models.ScheduleViewModel
{
    public class ScheduleViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public SelectList Departments { get; set; }
        public int selectedDeptID { get; set; }
        public List<Schedule> GetSchedules { get; set; }
        public string HttpMethod { get; set; }
    }
}
