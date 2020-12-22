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
    public class PupilsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PupilsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("api/AttachPupilAccount")]
        public async Task AttachPupilAccount([FromBody] JsonDocument request)
        {
            if(Request.Headers.GetCommaSeparatedValues("Authorization").ToList().Count<1)
            {
                Response.StatusCode = 403;
                return;
            }

            JObject jValue = WebMessageHelpers.GetJObjectFromBody(request);

            string userLogin = jValue.GetValue("login").ToString();
            int pupilId = Int32.Parse(jValue.GetValue("pupilId").ToString());
            string token = Request.Headers.GetCommaSeparatedValues("Authorization").ToList().ElementAt(0);

            if(UserHelpers.GetUser(token,_context).AccessLevel < (int)Permissions.Teacher)
                {
                    Response.StatusCode = 403;
                    return;
                }

            Response.ContentType = "application/json";
            byte[] body;
            var pupil = _context.Pupil.FirstOrDefault(row => row.Id == pupilId);
            User user = _context.User.FirstOrDefault(row => row.Login == userLogin);
            if (user == null || pupil == null)
            {
                Response.StatusCode = 400;
                body = UserHelpers.UserOrPupilAbsent();
                await Response.Body.WriteAsync(body, 0, body.Length);
                return;
            }

            if (pupil.AccountId != 0)
            {
                Response.StatusCode = 400;
                body = UserHelpers.PupilAlreadySynced();
                await Response.Body.WriteAsync(body, 0, body.Length);
                return;
            }

            pupil.AccountId = user.Id;
            user.AccessLevel = (int)Permissions.Pupil;
            await _context.SaveChangesAsync();
            Response.StatusCode = 200;
        }

        private bool PupilExists(int id)
        {
            return _context.Pupil.Any(e => e.Id == id);
        }
    }
}
