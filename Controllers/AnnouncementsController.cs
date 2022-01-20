using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Authorization;
using ProiectPractica.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

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
            DbSet<Announcement> announcements = _announcementsService.Get();
            if (announcements != null)
                if (announcements.ToList().Count > 0)
                {
                    return StatusCode(200, _announcementsService.Get());
                }
            return StatusCode(404);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Announcement announcement) //adauga inregistrare in tabel
        {
            try
            {
                if (announcement != null)
                {
                    _announcementsService.Post(announcement);
                    return StatusCode(201, Constants.CreateAnnouncement);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Announcement announcement) //updateaza inregistrare in tabel
        {
            try
            {
                if (announcement != null)
                {
                    _announcementsService.Put(announcement);
                    return StatusCode(204, Constants.UpdateAnnouncemet);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
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
                if (announcement != null)
                {
                    _announcementsService.Put(announcement);
                    return StatusCode(204, Constants.DeleteAnnouncement);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
