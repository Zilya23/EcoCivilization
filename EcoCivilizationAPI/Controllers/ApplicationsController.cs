﻿using System;
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

            return application;
        }

        // PUT: api/Applications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Applications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication([FromHeader] string token, Application application)
        {
            if(Convert.ToBoolean(_tokenService.CheckToken(token)))
            {
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetApplication", new { id = application.Id }, application);
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
