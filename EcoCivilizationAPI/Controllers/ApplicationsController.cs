using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoCivilizationAPI.Models;
using EcoCivilizationAPI.Service;
using NuGet.Common;

namespace EcoCivilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly EcoCivilizationContext _context;
        private TokenService _tokenService;

        public ApplicationsController(EcoCivilizationContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .ToListAsync();
        }

        [Route("GetСurrentApplication")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetСurrentApplications()
        {
            return await _context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .Where(x => x.IsBanned != true && x.IsDelete!= true)
                                              .ToListAsync();
        }

        [Route("GetFilteredApplication")]
        [HttpPost]
        public ActionResult<IEnumerable<Application>> GetFilteredApplications(Application application)
        {
            var filterApplication = _context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .Where(x => x.IsBanned != true && x.IsDelete != true)
                                              .ToList();
            if(application.Name.Length != 0)
            {
                filterApplication = filterApplication.Where(x=> x.Name.Contains(application.Name)).ToList();
            }

            if(application.IdCity > 0)
            {
                filterApplication = filterApplication.Where(x => x.IdCity == application.IdCity).ToList();
            }

            return filterApplication;
        }


        [Route("GetCheckBunnetApplication")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetCheckBunnetApplications()
        {
            return await _context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .Where(x => x.AppealCount > 0 && x.IsBanned != true)
                                              .ToListAsync();
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public ActionResult<Application> GetApplication(int id)
        {
            var application = _context.Applications.Include(x => x.PhotoApplications)
                                                   .Include(x => x.ApplicationUsers)
                                                   .Include(x => x.IdCityNavigation)
                                                   .ToList().FirstOrDefault(x => x.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            application.Date = new DateTime(application.Date.Value.Year, application.Date.Value.Month, application.Date.Value.Day, application.TimeStart.Value.Hours, application.TimeStart.Value.Minutes, 0);

            return application;
        }

        [Route("UserCreateApplication")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Application>>> GetUserCreateApplication([FromHeader] string token, User user)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                return await _context.Applications.Include(x => x.PhotoApplications)
                                              .Include(x => x.ApplicationUsers)
                                              .Where(x => x.IdUser == user.Id && x.IsDelete != true && x.IsBanned != true)
                                              .ToListAsync();
            }
            return Unauthorized();
        }

        // PUT: api/Applications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication([FromHeader]string token, int id, Application application)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                if (id != application.Id)
                {
                    return BadRequest();
                }

                Application editApplication = _context.Applications.FirstOrDefault(x => x.Id == id);
                editApplication.Name = application.Name;
                editApplication.Date = application.Date;
                editApplication.TimeStart = new TimeSpan(Convert.ToInt32(application.Date.Value.Hour), Convert.ToInt32(application.Date.Value.Minute), 0);
                editApplication.CountUser= application.CountUser;
                editApplication.Place= application.Place;
                editApplication.Description = application.Description;
                editApplication.IdCity = application.IdCity;

                _context.Entry(editApplication).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(editApplication);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Unauthorized();
        }

        // POST: api/Applications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication([FromHeader] string token, Application application)
        {
            if(Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                application.TimeStart = new TimeSpan(Convert.ToInt32(application.Date.Value.Hour), Convert.ToInt32(application.Date.Value.Minute), 0);
                application.AppealCount = 0;
                application.IsBanned= false;
                application.IsDelete= false;
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetApplication", new { id = application.Id }, application);
            }
            return Unauthorized();
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteApplication([FromHeader] string token, Application application)
        {
            if (Convert.ToBoolean(_tokenService.CheckToken(token)))
            {

                Application editApplication = _context.Applications.FirstOrDefault(x => x.Id == application.Id);
                editApplication.IsDelete = true;

                _context.Entry(editApplication).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(editApplication);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Unauthorized();
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
