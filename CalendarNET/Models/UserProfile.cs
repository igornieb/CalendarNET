using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CalendarNET.Models
{
    public class UserProfile : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [JsonIgnore]
        public ICollection<Models.Task>? Tasks { get; set; }

    }
}
