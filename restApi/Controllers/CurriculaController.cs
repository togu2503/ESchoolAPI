using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using restApi.Helpers;
using restApi.DAL;
using restApi.Models;

namespace restApi.Controllers
{
    [Route("api/GetTimeTable")]
    [ApiController]
    public class CurriculaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CurriculaController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Curricula
        [HttpGet]
        public async Task GetCurriculum()
        {
            var curiculum = _context.Curriculum.ToList();
            var lessons = _context.Lesson.ToList();
            var forms = _context.Form.ToList();

            byte [] body = CuriculumHelpers.GetCurriculumResponse(curiculum, lessons, forms);
            await Response.Body.WriteAsync(body, 0, body.Length);

        }

        // GET: api/Curricula/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curriculum>> GetCurriculum(int id)
        {
            var curriculum = await _context.Curriculum.FindAsync(id);

            if (curriculum == null)
            {
                return NotFound();
            }

            return curriculum;
        }

        // PUT: api/Curricula/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurriculum(int id, Curriculum curriculum)
        {
            if (id != curriculum.Id)
            {
                return BadRequest();
            }

            _context.Entry(curriculum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurriculumExists(id))
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

        // POST: api/Curricula
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Curriculum>> PostCurriculum(Curriculum curriculum)
        {
            _context.Curriculum.Add(curriculum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurriculum", new { id = curriculum.Id }, curriculum);
        }

        // DELETE: api/Curricula/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Curriculum>> DeleteCurriculum(int id)
        {
            var curriculum = await _context.Curriculum.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            _context.Curriculum.Remove(curriculum);
            await _context.SaveChangesAsync();

            return curriculum;
        }

        private bool CurriculumExists(int id)
        {
            return _context.Curriculum.Any(e => e.Id == id);
        }
    }
}
