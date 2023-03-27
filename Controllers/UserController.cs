using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using tarefasbackend.Models;
using tarefasbackend.Models.ViewModels;

namespace tarefasbackend.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody]User model)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            _userRepository.Create(model);

            return Ok();
            
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            User user = _userRepository.Read(model.Email, model.Password);
            
            if(user == null)
                return Unauthorized();
            
            user.Password = "";
            return Ok( new {
                user = user,
                token = GenerateToken(user) 
            });
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
           

            var key = "AGreatTokenAndThatsIT";
            var keyToByte = Encoding.ASCII.GetBytes(key);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    
                }),

                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyToByte), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    } 
}