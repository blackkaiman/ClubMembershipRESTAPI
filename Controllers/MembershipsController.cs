using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipsController : ControllerBase
    {
        private readonly ILogger<MembershipsController> _logger;
        private readonly ClubMembershipDbContext _context;
        public MembershipsController(ILogger<MembershipsController> logger, ClubMembershipDbContext context)
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
        public IActionResult Post([FromBody] Membership membership) //adauga inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    var _membership = new Membership()
                    {
                        IdMember = membership.IdMember,
                        IdMembership = membership.IdMembership,
                        IdMembershipType = membership.IdMembershipType,
                        EndDate = membership.EndDate,
                        StartDate = membership.StartDate,
                        Level = membership.Level
                    };
                    context.Entry(_membership).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
        public IActionResult Put([FromBody] Membership membership) //updateaza inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Update(membership);
                    context.SaveChanges();
                }
                return StatusCode(204, "Membership was updated from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Membership membership) //sterge inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Remove(membership);
                    context.SaveChanges();
                }
                return StatusCode(204, "Membership was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
