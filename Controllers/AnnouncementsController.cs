using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Authorization;
using ProiectPractica.Services;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AnnouncementsController : ControllerBase
    {
        private readonly ILogger<AnnouncementsController> _logger;
        private readonly IAnnouncementService _announcementsService;
        public AnnouncementsController(ILogger<AnnouncementsController> logger, IAnnouncementService announcementsService)
        {
            _logger = logger;
            _announcementsService = announcementsService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            if(_announcementsService != null)
            {
                return StatusCode(200, _announcementsService.Get());
            }
            else
            {
                return StatusCode(400, "No announcements were found!");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] Announcement announcement) //adauga inregistrare in tabel
        {
            try
            {
                _announcementsService.Post(announcement);
               return StatusCode(201, "Announcement was added in database.");
                
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
                _announcementsService.Put(announcement);
                return StatusCode(204, "Announcement was updated in swagger");
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
                _announcementsService.Delete(announcement);
                return StatusCode(204, "Announcement was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
