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
using static EcoCivilizationAPI.Controllers.UsersController;

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

        [Route("GetСurrentUser")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetСurrentUsers()
        {
            return await _context.Users.Where(x => x.IsBanned != true && x.IsDelete != true)
                                              .ToListAsync();
        }

        [Route("GetCheckBunnetUser")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetCheckBunnetUsers()
        {
            return await _context.Users.Where(x => x.IsBanned != true && x.CountBannedApplication > 0).ToListAsync();

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public  ActionResult<User> GetUser(int id)
        {
            var user =  _context.Users.Include(x=> x.IdCityNavigation).FirstOrDefault(x=> x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromHeader] string token, int id, User user)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                
                if (id != user.Id)
                {
                    return BadRequest();
                }
                User newUser = _context.Users.First(x => x.Id == user.Id);
                var users = _context.Users.ToList();
                users.Remove(newUser);

                bool exists = users.Any(e => e.Email == user.Email || e.Telephone == user.Telephone);
                if(!exists)
                {
                    newUser.Name = user.Name;
                    newUser.Email = user.Email;
                    newUser.Surname= user.Surname;
                    newUser.Telephone= user.Telephone;
                    newUser.IdGender= user.IdGender;
                    newUser.IdCity= user.IdCity;
                    newUser.Password= user.Password;
                    _context.Entry(newUser).State = EntityState.Modified;

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
                return Ok("UserNoExists");
            }
            return Unauthorized();
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
                CountBannedApplication = 0,
                IsDelete= false,
                IsBanned= false,
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

        [Route("Mail")]
        [HttpPost]
        public ActionResult<string> SendMail([FromHeader] string token, Mail mail)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                string senderEmail = "EcoCivilizationApp@yandex.com";
                string senderPassword = "afiaedbnreftftdp";

                SmtpClient smtpClient = new SmtpClient("smtp.yandex.com", 25);
                smtpClient.EnableSsl = true;
                var basicCredential = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.Credentials = basicCredential;

                // Создание объекта MailMessage
                MailMessage mailMessage = new MailMessage(senderEmail, mail._recipientEmail, mail._subject, mail._body);

                try
                {
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
            return Unauthorized();
        }

        [Route("Statistic")]
        [HttpPost]
        public ActionResult<UserStatistic> GetStatistic([FromHeader] string token, User user)
        {
            UserStatistic statistic = new UserStatistic();
            var userForStatistic = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                statistic.IdUser = user.Id;
                statistic.CountApplication = _context.Applications.Where(x=> x.IdUser == user.Id).Count();
                statistic.CountPartApplication = _context.ApplicationUsers.Where(x=> x.IdUser == user.Id).Count();
                statistic.CountUserCityApplication = _context.Applications.Where(x=> x.IdCity == userForStatistic.IdCity).Count();
                statistic.CountApplicationThisYear = _context.Applications.Where(x => ((DateTime)(x.Date)).Year == DateTime.Now.Year).Count();
                statistic.CountUserInCity = _context.Users.Where(x => x.IdCity == userForStatistic.IdCity).Count();
                statistic.CountActivDays = (int)(DateTime.Now - userForStatistic.DateRegist).Value.TotalDays;
                statistic.CountUserInApp = _context.Users.Where(x => x.IsDelete == false && x.IsBanned == false).Count();

                //Делаем топ юзеров
                List<User> users = _context.Users.Where(x=> x.IsBanned == false && x.IsDelete == false).ToList();
                foreach(var us in users)
                {
                    us.IdRole = _context.Applications.Where(x => x.IdUser == us.Id).Count();
                }
                users = users.OrderByDescending(x=> x.IdRole).ToList();

                List <City> cities = _context.Cities.ToList();

                foreach (var city in cities)
                {
                    city.TopPlace = _context.Applications.Where(x=> x.IdCity == city.Id).Count();
                }

                cities = cities.OrderByDescending(x=> x.TopPlace).ToList();

                statistic.CountTopUserPlace = users.FindIndex(x=> x.Id == userForStatistic.Id) + 1;
                statistic.CountTopUserCityPlace = cities.FindIndex(x=> x.Id == userForStatistic.IdCity) + 1;
                statistic.UsersTop = users;
                statistic.CityTop= cities;
                return Ok(statistic);
            }
            return Unauthorized();
        }

        public class Mail 
        {
            public string _recipientEmail { get; set; }
            public string _subject { get; set; }
            public string _body { get; set; }
        }

        public class UserStatistic
        {
            public int IdUser { get; set; }
            public int CountApplication { get; set; } // количество событий созданных пользователем
            public int CountPartApplication { get; set; } // количество участий пользователя
            public int CountUserCityApplication { get; set; } // количество событий в городе пользователя
            public int CountApplicationThisYear { get; set; } // количество событий созданных в этом году
            public int CountUserInCity { get; set; } // количество пользователей в городе пользователя
            public int CountActivDays { get; set; } // количество дней с регистрации пользователя
            public int CountUserInApp { get; set; } // количество пользователей в приложении
            public int CountTopUserPlace { get; set; } // место пользователя в топе по пользователям
            public int CountTopUserCityPlace { get; set; } // место города пользователя в топе по городам

            public List<User> UsersTop { get; set; }
            public List<City> CityTop { get; set; }
        }
    }
}
