using System.ComponentModel.DataAnnotations;

namespace CalendarNET.Controlers.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
