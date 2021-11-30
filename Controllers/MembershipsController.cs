using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using System.Text.Json;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembershipsController : ControllerBase
    {
        private readonly ILogger<MembershipsController> _logger;
        private readonly ClubMembershipDbContext _context;
        public MembershipsController(ILogger<MembershipsController> logger, ClubMembershipDbContext context)
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
        public IActionResult Post() //adauga inregistrare in tabel
        {
            return StatusCode(200);
        }

        [HttpPut]
        public IActionResult Put() //updateaza inregistrare in tabel
        {
            return StatusCode(200);
        }

        [HttpDelete]
        public IActionResult Delete() //sterge inregistrare in tabel
        {
            return StatusCode(200);
        }
    }
}
