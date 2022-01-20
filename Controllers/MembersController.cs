using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;
using ProiectPractica.Models;
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
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMembersService _membersService;
        public MembersController(ILogger<MembersController> logger, IMembersService membersService)
        {
            _logger = logger;
            _membersService = membersService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            DbSet<Member> members = _membersService.Get();
            if (members != null)
                if (members.ToList().Count > 0)
                {
                    return StatusCode(200, _membersService.Get());
                }
            return StatusCode(404);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Member member) //adauga inregistrare in tabel
        {
            try
            {
                if (member != null)
                {
                    _membersService.Post(member);
                    return StatusCode(201, Constants.CreateMember);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return StatusCode(500);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Member member) //updateaza inregistrare in tabel
        {
            try
            {
                if (member != null)
                {
                    _membersService.Put(member);
                    return StatusCode(204, Constants.UpdateMember);
                }
                return StatusCode((int)HttpStatusCode.NotFound);
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
                if (member != null)
                {
                    _membersService.Delete(member);
                    return StatusCode(204, Constants.DeleteMember);
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
