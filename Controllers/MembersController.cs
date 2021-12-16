using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;
using ProiectPractica.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly ClubMembershipDbContext _context;
        public MembersController(ILogger<MembersController> logger, ClubMembershipDbContext context)
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
        public IActionResult Post([FromBody] Member member) //adauga inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    var _member = new Member()
                    {
                        IdMember = Guid.NewGuid(),
                        Title = member.Title,
                        Name = member.Name,
                        Position = member.Position,
                        Description = member.Description,
                        Resume = member.Resume
                    };
                    context.Entry(_member).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    context.SaveChanges();
                    return StatusCode(201, "Member was added in database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Member member) //updateaza inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Update(member);
                    context.SaveChanges();
                }
                return StatusCode(204, "Member was updated from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Member member) //sterge inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Remove(member);
                    context.SaveChanges();
                }
                return StatusCode(204, "Member was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
