using CalendarNET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace CalendarNET.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set;} = DateTime.Now;
        public DateTime DueOn { get; set; } = DateTime.Now;
        public bool shared { get; set; } = false;
        public string UserId { get; set; }

        [JsonIgnore]
        public UserProfile User { get; set; }

    }
}
