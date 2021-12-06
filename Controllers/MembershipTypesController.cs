using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;
using ProiectPractica.Models;
using System;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        private readonly ILogger<MembershipTypesController> _logger;
        private readonly ClubMembershipDbContext _context;
        public MembershipTypesController(ILogger<MembershipTypesController> logger, ClubMembershipDbContext context)
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
        public IActionResult Post([FromBody] MembershipType membershipType) //adauga inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    var _membershipType = new MembershipType()
                    {
                        IdMembershipType = Guid.NewGuid(),
                        Description = membershipType.Description,
                        Name = membershipType.Name,
                        SubscriptionLengthinMonths = membershipType.SubscriptionLengthinMonths
                    };
                    context.Entry(_membershipType).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    context.SaveChanges();
                    return StatusCode(201, "Membership type was added in database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] MembershipType membershipType) //updateaza inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Update(membershipType);
                    context.SaveChanges();
                }
                return StatusCode(204, "Membership type was updated from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] MembershipType membershipType) //sterge inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Remove(membershipType);
                    context.SaveChanges();
                }
                return StatusCode(204, "Membership type was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
