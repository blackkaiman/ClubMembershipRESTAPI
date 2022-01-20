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
    public class MembershipsController : ControllerBase
    {
        private readonly ILogger<MembershipsController> _logger;
        private readonly IMembershipsService _membershipsService;
        public MembershipsController(ILogger<MembershipsController> logger, IMembershipsService membershipsService)
        {
            _logger = logger;
            _membershipsService = membershipsService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            DbSet<Membership> memberships = _membershipsService.Get();
            if (memberships != null)
                if (memberships.ToList().Count > 0)
                {
                    return StatusCode(200, _membershipsService.Get());
                }
            return StatusCode(404);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Membership membership) //adauga inregistrare in tabel
        {
            try
            {
                if (membership != null)
                {
                    _membershipsService.Post(membership);
                    return StatusCode(201, Constants.CreateMembership);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Membership membership) //updateaza inregistrare in tabel
        {
            try
            {
                if (membership != null)
                {
                    _membershipsService.Put(membership);
                    return StatusCode(204, Constants.UpdateMembership);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
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
                if (membership != null)
                {
                    _membershipsService.Delete(membership);
                    return StatusCode(204, Constants.DeleteMembership);
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
