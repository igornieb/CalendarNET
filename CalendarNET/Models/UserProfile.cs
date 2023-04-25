using Microsoft.AspNetCore.Identity;

namespace CalendarNET.Models
{
    public class UserProfile : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
