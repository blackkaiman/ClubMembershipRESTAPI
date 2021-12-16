using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Text.Json;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CodeSnippetsController : ControllerBase
    {
        private readonly ILogger<CodeSnippetsController> _logger;
        private readonly ClubMembershipDbContext _context;
        public CodeSnippetsController(ILogger<CodeSnippetsController> logger, ClubMembershipDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            return StatusCode(200, JsonSerializer.Serialize(_context.CodeSnippets));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodeSnippet codeSnippet) //adauga inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    var codeS = new CodeSnippet()
                    {
                        IdCodeSnippet = Guid.NewGuid(),
                        Title = codeSnippet.Title,
                        ContentCode = codeSnippet.ContentCode,
                        IdMember = codeSnippet.IdMember,
                        Revision = codeSnippet.Revision,
                        IsPublished = codeSnippet.IsPublished,
                        DateTimeAdded = DateTime.Now
                    };
                    context.Entry(codeS).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    context.SaveChanges(); 
                    return StatusCode(201, "Code snippet was added in database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] CodeSnippet codeSnippet) //updateaza inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Update(codeSnippet);
                    context.SaveChanges();
                }
                return StatusCode(204, "Code snippet was updated from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] CodeSnippet codeSnippet) //sterge inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Remove(codeSnippet);
                    context.SaveChanges();
                }
                return StatusCode(204, "Code snippet was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
