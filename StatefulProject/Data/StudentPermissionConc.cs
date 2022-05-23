using Microsoft.EntityFrameworkCore;

namespace StatefulProject.Data
{
    public class StudentPermissionConc:IStudentPermission
    {
        private ApplicationDbContext Context { get; set; }
        public StudentPermissionConc(ApplicationDbContext _context)
        { 
            Context =  _context;
        }

        public List<StudentPermission> getAllPermissions()
        {
            return Context.StudentPermissions.ToList();
        }
    }
}
