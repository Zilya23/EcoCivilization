using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoCivilizationAPI.Models;
using EcoCivilizationAPI.Service;

namespace EcoCivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly EcoCivilizationContext _context;
        private TokenService _tokenService;

        public ApplicationUsersController(EcoCivilizationContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/ApplicationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetApplicationUsers()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        // GET: api/ApplicationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(int id)
        {
            var applicationUser = await _context.ApplicationUsers.FindAsync(id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            return applicationUser;
        }

        // Выдает какие пользователи подписались на событие и инфа о них
        [Route("GetPartsUser")]
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationUser>> GetPartUserApplication(int id)
        {
            var applicationUser =_context.ApplicationUsers.Where(x => x.IdApplication == id).Include(x => x.IdUserNavigation).ToList();

            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/ApplicationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(int id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/ApplicationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostApplicationUser([FromHeader] string token, ApplicationUser applicationUser)
        {
            int userUD = _tokenService.CheckToken(token);

            if(userUD != 0)
            {
                applicationUser.IdUser = userUD;
                if (!ApplicationUserExists(applicationUser))
                {
                    applicationUser.Date = DateTime.Now;
                    _context.ApplicationUsers.Add(applicationUser);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
                }

                return NotFound();
            }
            
            return Unauthorized();
        }

        [Route("PartExists")]
        [HttpPost]
        public ActionResult<ApplicationUser> PartExists([FromBody] ApplicationUser applicationUser)
        {
            var i = _context.Applications.FirstOrDefault(x => x.Id == applicationUser.IdApplication);
            if (i.IdUser == applicationUser.IdUser)
                return NotFound();

            ApplicationUser exists = _context.ApplicationUsers.FirstOrDefault(u => u.IdUser == applicationUser.IdUser && u.IdApplication == applicationUser.IdApplication);

            if(exists != null)
            {
                return Ok(exists);
            }
            return NotFound();
        }

        [Route("UserPartApplication")]
        [HttpPost]
        public ActionResult<IEnumerable<Application>> GetUserPartApplication([FromHeader] string token, User user)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                var userPartApplication = _context.ApplicationUsers.Where(x => x.IdUser == user.Id).ToList();
                var userApplication = new List<Application>();

                foreach (var item in userPartApplication)
                {
                    userApplication.Add(_context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .Where(x=> x.Id == item.IdApplication && x.IsDelete != true && x.IsBanned != true)
                                              .FirstOrDefault());
                }

                return Ok(userApplication);
            }
            return Unauthorized();
        }

        private bool ApplicationUserExists(ApplicationUser applicationUser)
        {
            return _context.ApplicationUsers.Any(u => u.IdUser == applicationUser.IdUser && u.IdApplication == applicationUser.IdApplication);
        }

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromHeader] string token, int id)
        {
            int userUD = _tokenService.CheckToken(token);

            if (userUD != 0)
            {
                var applicationUser = await _context.ApplicationUsers.FindAsync(id);
                if (applicationUser == null)
                {
                    return NotFound();
                }

                _context.ApplicationUsers.Remove(applicationUser);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return Unauthorized();
        }

        [Route("AdminDeletePart")]
        [HttpPost]
        public IActionResult DeleteUserPart([FromHeader] string token, int id)
        {
            int userUD = _tokenService.CheckToken(token);

            if (userUD != 0)
            {
                var applicationUser =  _context.ApplicationUsers.Where(x => x.IdApplication == id).ToList(); ;
                if (applicationUser == null)
                {
                    return Ok('g');
                }

                foreach(var part in applicationUser)
                {
                    _context.ApplicationUsers.Remove(part);
                }
                
                 _context.SaveChangesAsync();

                return Ok();
            }

            return Unauthorized();
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
