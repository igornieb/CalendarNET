using CalendarNET.Controlers.Requests;
using CalendarNET.Data;
using CalendarNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CalendarNET.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public UserController(ApplicationDbContext context, UserManager<UserProfile> userManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
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
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginRequest request)
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
                    var userDB = _context.Users.FirstOrDefault(usr => usr.UserName == request.Username);

                    var accessToken = _tokenService.CreateToken(userDB);
                    await _context.SaveChangesAsync();
                    return Ok(new LoginResponse
                    {
                        Username = userDB.UserName,
                        Token = accessToken,
                    });
                }
                return BadRequest("Bad credentials");
            }
            return BadRequest();
        }

        //get current user info
        [HttpGet, Authorize]
        public async Task<ActionResult<UserResponse>> UserInfo()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return new UserResponse { Username = user.UserName, Email= user.Email, FirstName=user.Firstname, LastName=user.Lastname};
        }

        //set new password and/or change name
        [HttpPut, Authorize]
        public async Task<ActionResult<UserResponse>> UserEdit(UserRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Firstname = request.FirstName;
            user.Lastname = request.LastName;
            if(request.NewPassword != null && request.CurrentPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);


                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }
            await _userManager.UpdateAsync(user);
            return new UserResponse { Username = user.UserName, Email = user.Email, FirstName = user.Firstname, LastName = user.Lastname };
        }

        //delete account
        [HttpDelete, Authorize]
        public async Task<ActionResult<UserResponse>> UserDelete()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
    }
}