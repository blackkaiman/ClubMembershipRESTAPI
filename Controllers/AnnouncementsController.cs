using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System.Text.Json;
using System;
namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly ILogger<AnnouncementsController> _logger;
        private readonly ClubMembershipDbContext _context;
        public AnnouncementsController(ILogger<AnnouncementsController> logger, ClubMembershipDbContext context)
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
        public IActionResult Post([FromBody] Announcement announcement) //adauga inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    var _announcement = new Announcement()
                    {
                        IdAnnouncement = Guid.NewGuid(),
                        Tags = announcement.Tags,
                        Text = announcement.Text,
                        EventDateTime = DateTime.Now,
                        Title = announcement.Title,
                        ValidFrom = DateTime.Now,
                        ValidTo = DateTime.Now
                    };
                    context.Entry(announcement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    context.SaveChanges();
                    return StatusCode(201, "Announcement was added in database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Announcement announcement) //updateaza inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Update(announcement);
                    context.SaveChanges();
                }
                return StatusCode(204, "Announcement was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Announcement announcement) //sterge inregistrare in tabel
        {
            try
            {
                using (var context = _context)
                {
                    context.Remove(announcement);
                    context.SaveChanges();
                }
                return StatusCode(204, "Announcement was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
