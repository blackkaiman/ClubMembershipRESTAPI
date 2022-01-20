using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;
using ProiectPractica.Models;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ProiectPractica.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            DbSet<MembershipType> membershipTypes = _membershipTypesService.Get();
            if (membershipTypes != null)
                if (membershipTypes.ToList().Count > 0)
                {
                    return StatusCode(200, _membershipTypesService.Get());
                }
            return StatusCode(404);

        }

        [HttpPost]
        public IActionResult Post([FromBody] MembershipType membershipType) //adauga inregistrare in tabel
        {
            try
            {
                if (membershipType != null)
                {
                    _membershipTypesService.Post(membershipType);
                    return StatusCode(201, Constants.CreateMembershipType);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult Put([FromBody] MembershipType membershipType) //updateaza inregistrare in tabel
        {
            try
            {
                if (membershipType != null)
                {
                    _membershipTypesService.Put(membershipType);
                    return StatusCode(204, Constants.UpdateMembershipType);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
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
                if (membershipType != null)
                {
                    _membershipTypesService.Delete(membershipType);
                    return StatusCode(204, Constants.DeleteMembershipType);
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
