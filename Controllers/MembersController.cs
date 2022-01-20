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
                _membersService.Post(member);
                return StatusCode(202, "Member added in the database!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Member member) //updateaza inregistrare in tabel
        {
            try
            {
                _membersService.Put(member);
                return StatusCode(203, "Member was updated from swagger");
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
                _membersService.Delete(member);
                return StatusCode(204, "Member successfully deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
