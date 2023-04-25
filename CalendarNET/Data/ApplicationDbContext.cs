using CalendarNET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalendarNET.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserProfile>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Models.Task> TaskCollection { get; set; }



    }
}