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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("api/SignUp")]
        public async Task PostUser([FromBody] JsonDocument request)
        {
            JObject jValue = WebMessageHelpers.GetJObjectFromBody(request);
            User user = new User(0, jValue.GetValue("login").ToString(), jValue.GetValue("password").ToString());
            user.AccessLevel = 0;
            Response.ContentType = "application/json";
            byte[] body;

            user.Password = UserHelpers.HashPassword(user.Login, user.Password);

            if (_context.User.FirstOrDefault(row => row.Login == user.Login) != null)
            {
                Response.StatusCode = 400;
                body = UserHelpers.DuplicateUserResponse();    
                await Response.Body.WriteAsync(body, 0, body.Length);
                return;
            }
            
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            
            body = UserHelpers.SuccessfulUserAdding();
            Response.StatusCode = 200;
            await Response.Body.WriteAsync(body, 0, body.Length);
        }

        [HttpDelete("api/DeleteUser")]
        public async Task DeleteUser([FromBody] JsonDocument request)
        {
            byte[] body;
            JObject jValue = WebMessageHelpers.GetJObjectFromBody(request);
            string[] token = Request.Headers.GetCommaSeparatedValues("Authorization");
            if(token.Count()==0)
            {
                Response.StatusCode = 403;
                return;
            }
            var user = UserHelpers.GetUser(token[0], _context);
            if (user == null)
            {
                Response.StatusCode = 400;
                return;
            }

            var tempLogin = jValue.GetValue("login").ToString();

            var tempPassword = jValue.GetValue("password").ToString();

            if(user.Login != tempLogin && user.Password != UserHelpers.HashPassword(tempLogin,tempPassword))
            {
                Response.StatusCode = 400;
                body = UserHelpers.DuplicateUserResponse();
                await Response.Body.WriteAsync(body, 0, body.Length);
            }
            
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            body = UserHelpers.SuccessDeleting();
            await Response.Body.WriteAsync(body, 0, body.Length);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
