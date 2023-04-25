using Microsoft.Build.Framework;

namespace CalendarNET.Controlers.Requests
{
    public class UserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }        
        public string? NewPassword { get; set; } = null;
        public string? CurrentPassword { get; set; } = null;

    }
}
