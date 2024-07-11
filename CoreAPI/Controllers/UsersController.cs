using Core;
using CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        // GET: api/<UsersController>
        //[HttpGet]
        //public List<Core.User> Get()
        //{
        //    using (var con = new SqlConnection(Core.Program.ConnString))
        //    {
        //        con.Open();
        //    }
        //}

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public Core.User Get(int id)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                return Core.User.GetUser(con, id);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (user == null) return BadRequest("El usuario es null");

            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();

                User newUser = new Core.User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Rol = user.Rol
                };

                Core.User.AddUser(connection, newUser);
            }

            return Ok("Se creó el usuario exitosamente.");
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel user)
        {
            if (user == null || user.Id != id) return BadRequest("El producto es null o el ID no es igual.");

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();

                User newUser = new User()
                {
                    Id = id,
                    Username = user.Username,
                    Password = user.Password,
                    Rol = user.Rol
                };

                Core.User.UpdateUser(con, newUser);
            }

            return Ok("El producto se actualizó exitosamente.");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(Core.Program.ConnString))
                {
                    connection.Open();

                    Core.User user = new Core.User() { Id = id };
                    Core.User.DeleteUser(connection, (int)user.Id);
                }
            }
            catch (Exception)
            {
                return BadRequest("No se pudo eliminar el usuario.");
            }

            return Ok("El producto se eliminó exitosamente.");
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null) return BadRequest("El login es null");

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                var user = Core.User.LogIn(con, login.Username, login.Password);

                if (user == null || user.Id == 0)
                {
                    return Unauthorized("Usuario o contraseña incorrectos.");
                }

                return Ok(user);
            }
        }
    }
}
