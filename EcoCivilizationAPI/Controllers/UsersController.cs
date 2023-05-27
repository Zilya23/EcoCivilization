using System;
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
using EcoCivilizationAPI.Service;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace EcoCivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcoCivilizationContext _context;
        private readonly IConfiguration _configuration;
        private TokenService _tokenService;

        public UsersController(EcoCivilizationContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
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

        private bool UserExists(string email, string telephone)
        {
            return _context.Users.Any(e => e.Email == email || e.Telephone == telephone);
        }

        // POST: api/Users/login
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] User user)
        {
            var userLog = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if (userLog == null)
            {
                return NotFound();
            }
            //Проверка правильности пароля (С хешированием)

            string token = _tokenService.GetToken(userLog);


            return Ok(new {token = token});
        }


        // POST: api/Users/token
        [Route("getIdentifier")]
        [HttpPost]
        public ActionResult<int> GetUserIdentifier([FromBody] User token)
        {
            var userID = _tokenService.CheckToken(token.UserPhoto);
            if(userID != 0)
                return Ok(userID);

            return NotFound();
        }


        // POST: api/Users/registration
        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<User>> Regist([FromBody] User user)
        {
            User newUser = new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Telephone = user.Telephone,
                IdCity = user.IdCity,
                IdGender = user.IdGender,
                IdRole = 2,
                Email = user.Email,
                Password = user.Password,
                DateRegist = DateTime.Now,
                CountApplication = 0,
            };

            if (!UserExists(user.Email, user.Telephone))
            {
                var userSave = await _context.Users.AddAsync(newUser);
                var sucsessfull = await _context.SaveChangesAsync();
                if (Convert.ToBoolean(sucsessfull))
                    return Ok();
                else
                    return Conflict(); // если пользователь почему-то не сохранился
            }
            else
                return NotFound(); // если логин или телефон не уникальны
        }

        [Route("mail")]
        [HttpPost]
        public ActionResult<string> SendMail([FromHeader] string token, Mail mail)
        {
            string senderEmail = "EcoCivilizationApp@yandex.com";
            string senderPassword = "afiaedbnreftftdp";

            SmtpClient smtpClient = new SmtpClient("smtp.yandex.com", 25);
            smtpClient.EnableSsl = true;
            var basicCredential = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.Credentials = basicCredential;
            //smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            // Создание объекта MailMessage
            MailMessage mailMessage = new MailMessage(senderEmail, mail._recipientEmail, mail._subject, mail._body);

            try
            {
                // Создание объекта MailMessage
                //mailMessage = new MailMessage(senderEmail, mail._recipientEmail, mail._subject, mail._body);

                // Отправка письма
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        public class Mail 
        {
            public string _recipientEmail { get; set; }
            public string _subject { get; set; }
            public string _body { get; set; }
        }

    }
}
