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
    [Route("api/SignUp")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task PostUser([FromBody] JsonDocument request)
        {
            JObject jValue = JObject.Parse(request.RootElement.ToString());
            User user = new User(0, jValue.GetValue("login").ToString(), jValue.GetValue("password").ToString());
            user.Permissions = "0";
            Response.ContentType = "application/json";
            byte[] body;

            user.Password = UserHelpers.HashPassword(user.Login, user.Password);

            if (_context.User.FirstOrDefault(row => row.Login == user.Login) != null)
            {
                body = UserHelpers.DuplicateUserResponse();    
                await Response.Body.WriteAsync(body, 0, body.Length);
                return;
            }
            
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            
            body = UserHelpers.SuccessfulUserAdding();
            await Response.Body.WriteAsync(body, 0, body.Length);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
