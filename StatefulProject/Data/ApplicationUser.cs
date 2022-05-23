using Microsoft.AspNetCore.Identity;
using StatefulProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatefulProject.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public virtual Student Student { get; set; }
    }
}
