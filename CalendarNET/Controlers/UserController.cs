using CalendarNET.Controlers.Requests;
using CalendarNET.Data;
using CalendarNET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalendarNET.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context, UserManager<UserProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST api/<UserController>/register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<RegistrationRequest>> RegisterUser(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(new UserProfile { EmailConfirmed=true, UserName=request.Username, Email=request.Email, Firstname=request.Firstname, Lastname=request.Lastname}, request.Password);

            if (result.Succeeded)
            {
                return Created("", request);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserProfile>> LoginUser(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return BadRequest("Bad credentials");
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isPasswordValid)
                {
                    // TODO
                    //tu bedzie sie robil token
                    var userDB = _context.Users.FirstOrDefault(usr => usr.UserName == request.Username);
                    //tutaj zwroci ESSE
                    //
                    return user;
                }
                return BadRequest("Bad credentials");
            }
            return BadRequest();
        }


        //for debuging :)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.UserProfile>>> AllUsers()
        {
            return _userManager.Users.ToList();
        }
    }
}