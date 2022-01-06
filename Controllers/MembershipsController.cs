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
            if(_membershipsService != null)
            {
                return StatusCode(201, _membershipsService.Get());
            }
            else
            {
                return StatusCode(400, "No memberships were found!");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] Membership membership) //adauga inregistrare in tabel
        {
            try
            {

                _membershipsService.Post(membership);
                return StatusCode(202, "Membership succesfully added to the database!");
 
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
                _membershipsService.Put(membership);
                return StatusCode(203, "Membership updated!");
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
                _membershipsService.Delete(membership);
                return StatusCode(204, "Membership was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
