using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.RequestModels;

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
        public async Task<ActionResult<List<UserModel>>> GetUser()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("Added")]
        public async Task<ActionResult<List<UserModel>>> AddUser(UserModel usr)
        {
            
             _context.Users.Add(usr);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("AuthUser")]
        public IActionResult AuthUser(UserRequestModel usr)
        {
            var loginedUser = _context.Users.AsQueryable().FirstOrDefault(x=>x.UserName==usr.UserName&& x.Password == usr.Password);
            var varmi = loginedUser!=null ? true: false;

            if (varmi)
            {
                var tokenInfo = new TokenModel
                {
                    Token = _jwtAuthenticationManager.Authenticate(usr.UserName, usr.Password),
                    ExpireDate = DateTime.UtcNow.AddHours(3),
                    TokenType = loginedUser.UserType,
                    UserId = loginedUser.Id
                     
                };
            
            _context.Tokens.Add(tokenInfo);
            _context.SaveChanges();
                return Ok(tokenInfo);
            }  
            return Ok("user not found");
        }

    }
}
