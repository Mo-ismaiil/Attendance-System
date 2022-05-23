namespace StatefulProject.Data
{
    public interface ISchedule
    {
        public List<Schedule> GetSchedules(int DepartmentId, DateTime TimeRangeMin, DateTime TimeRangeMax);
        public void SetSchedules(List<Schedule> Schedules, Department DepartmentId);
    }
}
