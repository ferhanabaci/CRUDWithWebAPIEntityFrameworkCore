using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;

        public UsersController(JwtAuthenticationManager jwtAuthenticationManager, DataContext context)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("Added")]
        public async Task<ActionResult<List<User>>> AddUser(User usr)
        {
            
             _context.Users.Add(usr);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public IActionResult AuthUser(User usr)
        {
            var varmi = _context.Find<User>(usr.UserName,usr.Password);
            var token = _jwtAuthenticationManager.Authenticate(usr.UserName, usr.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}
