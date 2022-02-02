using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyShopsNear.Models;
using MyShopsNear.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MyShopsNear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;

        public static UserLogin user = new UserLogin();

        public static Users usr = new Users();

        private readonly IConfiguration _configuration;

     

        public UsersController(IUserServices userServices, IConfiguration configuration)
        {
            this.userServices = userServices;
            _configuration = configuration;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<List<Users>> Get()
        {
            return userServices.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{username}")]
        public ActionResult<Users> Get(string username)
        {
            try
            {
                var user = userServices.Get(username);

                if (user == null)
                {
                    return NotFound($"User with the Username {username} is not found");
                }

                return user;

            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users user)
        {
            try
            {
                userServices.Create(user);

                return CreatedAtAction(nameof(Get), new { username = user.Username }, user);

            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id,string username, [FromBody] Users user)
        {
            try
            {
                var existuser = userServices.Get(username); 

                if (existuser.Id == null || existuser.Id != user.Id)
                {
                    return NotFound($"User with Username = {username} not found");
                }

                userServices.Update(id, user);

                return Ok("Done");

            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id, string username)
        {
            try
            {
                var existuser = userServices.Get(username);

                if (existuser == null)
                {
                    return NotFound($"User with the Username {username} is not found");
                }

                userServices.Remove(username);

                return Ok($"User with Id = {id} deleted");

            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //Login

        [HttpPost("Login")]
        public ActionResult Login(UserLogin userLogin,string Email, string username)
        {
            try
            {
                var user = userServices.Get(username);

                if (user == null || user.Email != userLogin.Email)
                {
                    return NotFound($"User with the Username {Email} is not found");
                }

                string token = CreateToken(user);
                return Ok(token);

            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        
        //token creation
        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("appSettings:Key").ToString()));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
