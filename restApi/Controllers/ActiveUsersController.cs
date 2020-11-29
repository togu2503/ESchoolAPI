using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using restApi.DAL;
using restApi.Models;
using restApi.Helpers;

namespace restApi.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class ActiveUsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ActiveUsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // POST: api/ActiveUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task PostActiveUser([FromBody]JsonDocument request)
        {
            
            JObject jValue = JObject.Parse(request.RootElement.ToString());

            User userAuth = new User(0, jValue.GetValue("login").ToString(), jValue.GetValue("password").ToString());
            var user = _context.User.FirstOrDefault(row => row.Login == userAuth.Login);

            byte[] body = UserHelpers.BadLogin();

            if (user != null)
            {

                string hashPassword = UserHelpers.HashPassword(userAuth.Login, userAuth.Password);

                if(user.Password != hashPassword)
                {
                    await Response.Body.WriteAsync(body, 0, body.Length);
                    return;
                }

                var logedUser = _context.ActiveUser.FirstOrDefault(row => row.UserId == user.Id);
                string token = "";
                if (logedUser != null)
                    token = logedUser.Token;
                else
                {
                    token = UserHelpers.GenerateUserToken();
                    ActiveUser activeUser = new ActiveUser(0, user.Id, token);
                    _context.ActiveUser.Add(activeUser);
                    await _context.SaveChangesAsync();
                }
                
                body = UserHelpers.SuccessfulLogin(token);
                await Response.Body.WriteAsync(body, 0, body.Length);
            }
            else
            {
                await Response.Body.WriteAsync(body, 0, body.Length);
            }
        }

        private bool ActiveUserExists(int id)
        {
            return _context.ActiveUser.Any(e => e.Id == id);
        }
    }
}
