using Microsoft.AspNetCore.Mvc;
using MyShopsNear.Models;
using MyShopsNear.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopsNear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;

        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
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
            var user = userServices.Get(username);

            if (user == null)
            {
                return NotFound($"Student with the Username {username} is not found");
            }

            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users user) 
        {
                userServices.Create(user);

                return CreatedAtAction(nameof(Get), new { username = user.Username }, user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Users user)
        {
            var existingStudent = userServices.Get(id);

            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            userServices.Update(id, user);

            return Ok("Done");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var user = userServices.Get(id);

            if (user == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            userServices.Remove(user.Id);

            return Ok($"Student with Id = {id} deleted");
        }
    }
}
