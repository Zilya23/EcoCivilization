using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoCivilizationAPI.Models;

namespace EcoCivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcoCivilizationContext _context;

        public UsersController(EcoCivilizationContext context)
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

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
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

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool UserExists(string login, string password, string telephone)
        {
            return _context.Users.Any(e => (e.Login == login && e.Password == password) || e.Telephone == telephone);
        }

        [HttpGet("{login, password}")]
        public async Task<ActionResult<User>> Login(string login, string password)
        {
            var user = await _context.Users.FindAsync(login, password);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{name, surname, telephone, idCity, idGender, idRole, login, password, dateRegist}")]
        public async Task<ActionResult<User>> Regist(string name, string surname, string telephone, int idCity,
                                                     int idGender, int idRole, string login, string password, DateTime dateRegist)
        {
            User newUser = new User();
            newUser.Name = name;
            newUser.Surname = surname;
            newUser.Telephone = telephone;
            newUser.IdCity= idCity; 
            newUser.IdGender = idGender; 
            newUser.IdRole = idRole;
            newUser.Login = login;
            newUser.Password = password;
            newUser.DateRegist = dateRegist;

            if (UserExists(newUser.Login, newUser.Password, newUser.Telephone))
            {
                var user = await _context.Users.AddAsync(newUser);
                var sucsessfull = await _context.SaveChangesAsync();
                if (!Convert.ToBoolean(sucsessfull))
                    return Ok(user);

                return Conflict(); // если пользователь почему-то не сохранился
            }
            else
                return NotFound(); // если логин пароль или телефон не уникальны
        }
    }
}
