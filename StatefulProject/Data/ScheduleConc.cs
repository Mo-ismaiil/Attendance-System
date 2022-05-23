namespace StatefulProject.Data
{
    public class ScheduleConc:ISchedule
    {
        private ApplicationDbContext DBContext { get; set; }
        //DB Context Dependancy Injection
        public ScheduleConc(ApplicationDbContext _Context)
        {
            DBContext = _Context;
        }
        //Implemented Methods
        public List<Schedule> GetSchedules(int DepartmentId, DateTime TimeRangeMin, DateTime TimeRangeMax)
        {
            return DBContext.Schedules.Where(S => (S.ScheduleDate >= TimeRangeMin) && (S.ScheduleDate <= TimeRangeMax) && (S.Department.Id == DepartmentId)).ToList();
        }

        public void SetSchedules(List<Schedule> Schedules, Department Department)
        {
            Schedules.ForEach(Sch =>
            {
                if (Sch.AddSchedule == 1)
                {
                    Sch.Department = Department;
                    Sch.DepartmentId = Department.Id;
                    if (Sch.Id == 0)
                    {
                        DBContext.Schedules.Add(Sch);
                    }
                    else
                    {
                        DBContext.Schedules.Update(Sch);
                    }
                }
                else
                {
                    if (Sch.Id != 0)
                        DBContext.Schedules.Remove(Sch);
                }
            });
            DBContext.SaveChanges();
        }
    }
}
