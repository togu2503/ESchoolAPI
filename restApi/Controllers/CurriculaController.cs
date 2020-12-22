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
    [Route("api")]
    [ApiController]
    public class CurriculaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CurriculaController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("/GetTimeTable")]
        public async Task GetCurriculum()
        {
            byte [] body = CuriculumHelpers.GetTimeTableWithoutHomeworkResponse(_context);
            await Response.Body.WriteAsync(body, 0, body.Length);

        }

        private bool CurriculumExists(int id)
        {
            return _context.Curriculum.Any(e => e.Id == id);
        }
    }
}
