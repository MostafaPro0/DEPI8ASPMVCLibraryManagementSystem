using Microsoft.AspNetCore.Identity;

namespace DEPI8ASPMVCLibraryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
