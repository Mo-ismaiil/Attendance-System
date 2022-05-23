namespace StatefulProject.Data
{
    public class DepartmentConc:IDepartment
    {
        private ApplicationDbContext context { get; set; }
        public DepartmentConc(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        //Implement IDepartment Methods.
        public IEnumerable<Department> getDepartments()
        {
            return context.Departments;
        }

        //sokaryy
        public List<Department> GetDepartment()
        {
            return context.Departments.ToList();
        }
        public Department getDepartmentById(int id)
        {
            return context.Departments.Where(Dep=>Dep.Id == id).FirstOrDefault();
        }
    }
}
