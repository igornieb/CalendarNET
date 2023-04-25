using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarNET.Data;
using CalendarNET.Controlers.Requests;
using Microsoft.AspNetCore.Identity;
using CalendarNET.Models;

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
        // returns today tasks
        [HttpGet]
        [Route("today")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTodayTaskCollection()
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            var today_tasks = await _context.TaskCollection.Where(task => task.DueOn.Date == DateTime.UtcNow.Date).ToListAsync();
            return today_tasks;

        }

        [HttpGet("{year}/{month}/{day}")]
        // GET: api/Task/2022/12/21
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetDateTaskCollection(int year, int month, int day)
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            DateTime date = DateTime.Parse($"{day}/{month}/{year}");
            var today_tasks = await _context.TaskCollection.Where(task => task.DueOn.Date == date.Date).ToListAsync();
            return today_tasks;
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTaskId(int id)
        {
          if (_context.TaskCollection == null)
          {
              return NotFound();
          }
            var task = _context.TaskCollection.FirstOrDefault(task => task.Id==id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, TaskRequest task)
        {
            var current_task = await _context.TaskCollection.FindAsync(id);
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
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(TaskRequest task)
        {
          if (_context.TaskCollection == null)
          {
              return Problem("Empty set");
          }

            var taskDb = new Models.Task { Name = task.Name, Description = task.Description, DueOn = task.DueOn, shared = task.shared };
            _context.TaskCollection.Add(taskDb);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTaskId", new { id = taskDb.Id }, taskDb);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.TaskCollection == null)
            {
                return NotFound();
            }
            var task = await _context.TaskCollection.FindAsync(id);
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
