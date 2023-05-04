﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoCivilizationAPI.Models;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        // POST: api/Users/login
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            if (user == null)
            {
                return NotFound();
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Authentication, user.Login) };

            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            claims: claims,
            audience: AuthOptions.AUDIENCE,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        //public string GetToken(User user)
        //{
        //    var SECRET_KEY = "cAtwa1kkEy";
        //    Header header = new Header(){alg="HS256", type="JWT"};
        //    var unsignedHeader = Convert.ToBase64String(ObjectToByteArray(header));
        //    var unsignedPayload = Convert.ToBase64String(ObjectToByteArray(user));
        //    var unsignedToken = unsignedHeader + '.' + unsignedPayload;
        //    var signature = HMAC.Create();



        //}

        // POST: api/Users/registration
        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<User>> Regist(string name, string surname, string telephone, int idCity,
                                                     int idGender, int idRole, string login, string password)
        {
            User newUser = new User
            {
                Name = name,
                Surname = surname,
                Telephone = telephone,
                IdCity = idCity,
                IdGender = idGender,
                IdRole = idRole,
                Login = login,
                Password = password,
                DateRegist = DateTime.Now
            };

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

    public class Header
    {
        public string alg { get; set; }
        public string type { get; set; }
    }
}
