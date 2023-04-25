using CalendarNET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CalendarNET.Models
{
    public class Task
    {
        [BindNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BindNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [BindNever]
        public DateTime UpdatedDate { get; set;} = DateTime.Now;
        public DateTime DueOn { get; set; } = DateTime.Now;
        public bool shared { get; set; } = false;

    }
}
