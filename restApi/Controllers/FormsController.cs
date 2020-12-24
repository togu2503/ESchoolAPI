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
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public FormsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Forms
        [HttpGet("api/GetAllForms")]
        public async Task GetForms()
        {
            if (Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count < 1)
            {
                Response.StatusCode = 403;
                return;
            }

            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if (UserHelpers.GetUser(token, _context).AccessLevel < (int)Permissions.Pupil)
            {
                Response.StatusCode = 403;
                return;
            }

            byte[] body = FormHelpers.GetAllFormsWithPupils(_context);
            await Response.Body.WriteAsync(body, 0, body.Length);
        }

        // GET: api/Forms/5
        [HttpGet("api/Forms/{id}")]
        public async Task<ActionResult<Form>> GetForm(int id)
        {
            if (Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count < 1)
            {
                Response.StatusCode = 403;
                return null;
            }

            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if (UserHelpers.GetUser(token, _context).AccessLevel < (int)Permissions.Pupil)
            {
                Response.StatusCode = 403;
                return null;
            }


            var form = await _context.Form.FindAsync(id);

            if (form == null)
            {
                Response.StatusCode = 400;
                return NotFound();
            }

            return form;
        }

        [HttpPut("api/Forms/{id}")]
        public async Task<IActionResult> PutForm(int id, Form form)
        {
            if (Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count < 1)
            {
                Response.StatusCode = 403;
                return null;
            }

            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if (UserHelpers.GetUser(token, _context).AccessLevel < (int)Permissions.Pupil)
            {
                Response.StatusCode = 403;
                return null;
            }


            if (id != form.Id)
            {
                return BadRequest();
            }

            _context.Entry(form).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(id))
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

        [HttpPost("api/CreateForm")]
        public async Task<ActionResult<Form>> PostForm(Form form)
        {
            if (Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count < 1)
            {
                Response.StatusCode = 403;
                return null;
            }

            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if (UserHelpers.GetUser(token, _context).AccessLevel < (int)Permissions.Pupil)
            {
                Response.StatusCode = 403;
                return null;
            }

            _context.Form.Add(form);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForm", new { id = form.Id }, form);
        }

        // DELETE: api/Forms/5
        [HttpDelete("api/Forms/{id}")]
        public async Task<ActionResult<Form>> DeleteForm(int id)
        {
            if (Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count < 1)
            {
                Response.StatusCode = 403;
                return null;
            }

            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if (UserHelpers.GetUser(token, _context).AccessLevel < (int)Permissions.Pupil)
            {
                Response.StatusCode = 403;
                return null;
            }

            var form = await _context.Form.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            _context.Form.Remove(form);
            await _context.SaveChangesAsync();

            return form;
        }

        private bool FormExists(int id)
        {
            return _context.Form.Any(e => e.Id == id);
        }
    }
}
