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
    public class CurriculaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CurriculaController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("api/GetTimeTable")]
        public async Task GetCurriculum()
        {
            byte [] body = CuriculumHelpers.GetTimeTableWithoutHomeworkResponse(_context);
            await Response.Body.WriteAsync(body, 0, body.Length);

        }

        [HttpGet("api/GetHomework/{id}")]
        public async Task GetCurriculum(int id, [FromBody] JsonDocument ? request)
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

            if(_context.Form.FirstOrDefault(row => row.Id == id) == null)
            {
                Response.StatusCode = 400;
                return;
            }

            DateTime date = DateTime.Now;

            JObject jValue = WebMessageHelpers.GetJObjectFromBody(request);
            if(jValue.ContainsKey("date"))
            {
                string strDate = jValue.GetValue("date").ToString();
                if (!String.IsNullOrWhiteSpace(strDate))
                    date = Convert.ToDateTime(strDate);
            }

            Response.Headers.Add("Access-Control-Allow-Headers", "*");
            Response.Headers.Add("Content-Type", "application/json");

            byte[] body = CuriculumHelpers.GetHomeWork(_context, id, date);
            await Response.Body.WriteAsync(body, 0, body.Length);

        }

        private bool CurriculumExists(int id)
        {
            return _context.Curriculum.Any(e => e.Id == id);
        }
    }
}
