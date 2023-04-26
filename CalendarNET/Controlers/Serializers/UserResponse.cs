using Microsoft.Build.Framework;

namespace CalendarNET.Controlers.Requests
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
