namespace StatefulProject.Data
{
    public interface IDepartment
    {
        //Add Department DB method signatures.
        IEnumerable<Department> getDepartments();
        Department getDepartmentById(int id);
        List<Department> GetDepartment();

    }
}
