using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Examen.Models;
using API_Examen.ModelsDTOs;

namespace API_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDbContext _context;

        public UsersController(AnswersDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUserInfoByEmail(string pEmail)
        {
            var query = (from u in _context.Users
                         join ur in _context.UserRoles
                         on u.UserRoleId equals ur.UserRoleId
                         where u.BackUpEmail == pEmail
                         select new
                         {
                             id = u.UserId,
                             correo = u.BackUpEmail,
                             nombreUsuario = u.UserName,
                             nombre = u.FirstName,
                             apellido = u.LastName,
                             telefono = u.PhoneNumber,
                             contrasennia = u.UserPassword,
                             rolid = u.UserRoleId,
                             descriprol = ur.UserRole1
                         }
                         ).ToList();

            List<UsuarioDTO> list = new List<UsuarioDTO>();
            foreach (var item in query)
            {
                UsuarioDTO nuevoUsuario = new UsuarioDTO()
                {
                    UsuarioID = item.id,
                    Correo = item.correo,
                    NombreUsuario = item.nombreUsuario,
                    Nombre = item.nombre,
                    Apellido = item.apellido,
                    Telefono = item.telefono,
                    Contrasennia = item.contrasennia,
                    RolID = item.rolid
                };

                list.Add(nuevoUsuario);
            }

            if (list == null || list.Count == 0)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Users/ValidateUser
        [HttpPost("ValidateUser")]
        public async Task<ActionResult<UsuarioDTO>> ValidateUser([FromBody] UserLoginRequest loginRequest)
        {
            // Consulta para verificar si el usuario existe y las credenciales coinciden
            var user = await _context.Users
                .Where(u => u.BackUpEmail == loginRequest.Email && u.UserPassword == loginRequest.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // Si no se encuentra el usuario, retornamos Unauthorized
                return Unauthorized(new { message = "Credenciales incorrectas." });
            }

            // Si el usuario existe, mapeamos los datos a un DTO y retornamos la información
            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                UsuarioID = user.UserId,
                Correo = user.BackUpEmail,
                Nombre = user.UserName,
                Telefono = user.PhoneNumber,
                Contrasennia = user.UserPassword,
                RolID = user.UserRoleId
            };

            return Ok(usuarioDTO);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
