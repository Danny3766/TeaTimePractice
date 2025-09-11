using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeaTime.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Name { get; set; }

        public string Address { get; set; }
    }
}
