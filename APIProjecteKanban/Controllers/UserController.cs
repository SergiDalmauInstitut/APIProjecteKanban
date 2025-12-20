using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIProjecteKanban.Controllers
{
    [EnableCors]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: users
        [HttpGet]
        public List<User> Get()
        {
            UserService objUserService = new();
            return objUserService.GetAll();
        }

        // GET users/5
        [HttpGet("{id}")]
        public User GetByID(int id)
        {
            UserService objUserService = new();
            return objUserService.GetById(id);
        }

        // POST users
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginDTO credentials)
        {
            UserService objUserService = new();

            User? user = objUserService.GetByMailPassword(credentials);

            if (user == null)
            {
                return Unauthorized(new { Message = "Correu electrònic o contrasenya incorrectes." });
            }

            return Ok(user);
        }

        // POST users
        [HttpPost]
        public User Post([FromBody] User user)
        {
            UserService objUserService = new();
            return objUserService.Add(user);
        }

        // PUT users/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] User user)
        {
            UserService objUserService = new();
            return objUserService.Update(id, user);
        }

        // DELETE users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            UserService objUserService = new();
            objUserService.Delete(id);
        }
    }
}
