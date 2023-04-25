using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarNET.Data;
using CalendarNET.Controlers.Requests;
using Microsoft.AspNetCore.Identity;
using CalendarNET.Models;
using Microsoft.AspNetCore.Authorization;

namespace CalendarNET.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserProfile> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<UserProfile> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        // GET: api/Tasks
        // return today tasks
        [HttpGet, Authorize]
        [Route("today")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTodayTaskCollection()
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var today_tasks = await _context.TaskCollection.Where(task => task.DueOn.Date == DateTime.UtcNow.Date && task.UserId == user.Id).ToListAsync();
            return today_tasks;

        }

        [HttpGet("{year}/{month}/{day}"), Authorize]
        // GET: api/Task/2022/12/21
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetDateTaskCollection(int year, int month, int day)
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            DateTime date = DateTime.Parse($"{day}/{month}/{year}");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var today_tasks = await _context.TaskCollection.Where(task => task.DueOn.Date == date.Date && task.UserId==user.Id).ToListAsync();
            return today_tasks;
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Models.Task>> GetTaskId(int id)
        {
          if (_context.TaskCollection == null)
          {
              return NotFound();
          }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task = _context.TaskCollection.FirstOrDefault(task => task.Id==id && task.UserId==user.Id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> EditTask(int id, TaskRequest task)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var current_task = await _context.TaskCollection.FirstOrDefaultAsync(task => task.Id == id && task.UserId == user.Id);
            if (current_task == null)
            {
                return NotFound();
            }

            current_task.Name = task.Name;
            current_task.Description = task.Description;
            current_task.DueOn = task.DueOn;
            current_task.shared = task.shared;
            current_task.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskId", new { id = current_task.Id }, current_task);
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<TaskRequest>> PostTask(TaskRequest task)
        {
          if (_context.TaskCollection == null)
          {
              return Problem("Empty set");
          }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var taskDb = new Models.Task { Name = task.Name, Description = task.Description, DueOn = task.DueOn, shared = task.shared, UserId=user.Id, User=user };
            _context.TaskCollection.Add(taskDb);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTaskId", new { id = taskDb.Id }, taskDb);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task = await _context.TaskCollection.FirstOrDefaultAsync(task => task.Id == id && task.UserId == user.Id);
            if (task == null)
            {
                return NotFound();
            }

            _context.TaskCollection.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
