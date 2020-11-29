using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restApi.DAL;
using restApi.Models;

namespace restApi.Controllers
{
    [Route("api/pupils")]
    [ApiController]
    public class PupilsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PupilsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Pupils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pupil>>> GetPupil()
        {
            return await _context.Pupil.ToListAsync();
        }

        // GET: api/Pupils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pupil>> GetPupil(int id)
        {
            var pupil = await _context.Pupil.FindAsync(id);

            if (pupil == null)
            {
                return NotFound();
            }

            return pupil;
        }

        // PUT: api/Pupils/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPupil(int id, Pupil pupil)
        {
            if (id != pupil.Id)
            {
                return BadRequest();
            }

            _context.Entry(pupil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PupilExists(id))
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

        // POST: api/Pupils
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pupil>> PostPupil(Pupil pupil)
        {
            _context.Pupil.Add(pupil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPupil", new { id = pupil.Id }, pupil);
        }

        // DELETE: api/Pupils/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pupil>> DeletePupil(int id)
        {
            var pupil = await _context.Pupil.FindAsync(id);
            if (pupil == null)
            {
                return NotFound();
            }

            _context.Pupil.Remove(pupil);
            await _context.SaveChangesAsync();

            return pupil;
        }

        private bool PupilExists(int id)
        {
            return _context.Pupil.Any(e => e.Id == id);
        }
    }
}
