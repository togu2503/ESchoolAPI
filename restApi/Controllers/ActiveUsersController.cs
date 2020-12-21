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
    [ApiController]
    public class ActiveUsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ActiveUsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("api/Login")]
        public async Task Login([FromBody]JsonDocument request)
        {

            JObject jValue = WebMessageHelpers.GetJObjectFromBody(request);
            Response.Headers.Add("Access-Control-Allow-Headers", "*");
            Response.Headers.Add("Content-Type", "application/json");
            User userAuth = new User(0, jValue.GetValue("login").ToString(), jValue.GetValue("password").ToString());
            var user = _context.User.FirstOrDefault(row => row.Login == userAuth.Login);

            byte[] body;

            if (user != null)
            {

                string hashPassword = UserHelpers.HashPassword(userAuth.Login, userAuth.Password);

                if(user.Password != hashPassword)
                {
                    Response.StatusCode = 401;
                    body = UserHelpers.WrongPasswordOrLogin();
                    await Response.Body.WriteAsync(body, 0, body.Length);
                    return;
                }

                var logedUser = _context.ActiveUser.FirstOrDefault(row => row.UserId == user.Id);
                string token = "";
                if (logedUser != null)
                {
                    token = logedUser.Token;
                }
                else
                {
                    token = UserHelpers.GenerateUserToken();
                    ActiveUser activeUser = new ActiveUser(0, user.Id, token);
                    _context.ActiveUser.Add(activeUser);
                    await _context.SaveChangesAsync();
                }
                
                body = UserHelpers.SuccessfulLogin(token);
                Response.StatusCode = 200;
                await Response.Body.WriteAsync(body, 0, body.Length);
            }
            else
            {
                Response.StatusCode = 401;
                body = UserHelpers.WrongPasswordOrLogin();
                await Response.Body.WriteAsync(body, 0, body.Length);
                return;
            }
        }

        [HttpPost("api/Logout")]

        public async Task Logout([FromBody] JsonDocument request)
        {
            string[] token = Request.Headers.GetCommaSeparatedValues("Authorization");
            if (token.Count() == 0)
            {
                Response.StatusCode = 403;
                return;
            }
            var user = _context.ActiveUser.Find(token);
            if (user == null)
            {
                Response.StatusCode = 400;
                return;
            }

            _context.ActiveUser.Remove(user);
            await _context.SaveChangesAsync();
            Response.StatusCode = 200;
        }

        private bool ActiveUserExists(int id)
        {
            return _context.ActiveUser.Any(e => e.Id == id);
        }
    }
}
