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
    public class PhotoApplicationsController : ControllerBase
    {
        private readonly EcoCivilizationContext _context;

        public PhotoApplicationsController(EcoCivilizationContext context)
        {
            _context = context;
        }

        // GET: api/PhotoApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoApplication>>> GetPhotoApplications()
        {
            return await _context.PhotoApplications.ToListAsync();
        }

        // GET: api/PhotoApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoApplication>> GetPhotoApplication(int id)
        {
            var photoApplication = await _context.PhotoApplications.FindAsync(id);

            if (photoApplication == null)
            {
                return NotFound();
            }

            return photoApplication;
        }

        // POST: api/PhotoApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<PhotoApplication>> PostPhotoApplication(PhotoApplication photoApplication)
        {
            _context.PhotoApplications.Add(photoApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhotoApplication", new { id = photoApplication.Id }, photoApplication);
        }

        // DELETE: api/PhotoApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoApplication(int id)
        {
            var photoApplication = await _context.PhotoApplications.FindAsync(id);
            if (photoApplication == null)
            {
                return NotFound();
            }

            _context.PhotoApplications.Remove(photoApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
