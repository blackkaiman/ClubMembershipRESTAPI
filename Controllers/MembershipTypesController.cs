using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;
using ProiectPractica.Models;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ProiectPractica.Services;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipTypesController : ControllerBase
    {
        private readonly ILogger<MembershipTypesController> _logger;
        private readonly IMembershipTypesService _membershipTypesService;

        public MembershipTypesController(ILogger<MembershipTypesController> logger, IMembershipTypesService membershipTypesService)
        {
            _logger = logger;
            _membershipTypesService = membershipTypesService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            if(_membershipTypesService != null)
            {
                return StatusCode(200,_membershipTypesService.Get());
            }
            else
            {
                return StatusCode(400, "No membership types were found!");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] MembershipType membershipType) //adauga inregistrare in tabel
        {
            try
            {
                _membershipTypesService.Post(membershipType);
                return StatusCode(201, "Added membership type to database!");
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
                _membershipTypesService.Put(membershipType);
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
                _membershipTypesService.Delete(membershipType);
                return StatusCode(204, "Membership type was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
