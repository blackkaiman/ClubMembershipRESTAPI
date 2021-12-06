using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectPractica.Models.Authentication;
using ProiectPractica.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);



            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });



            return Ok(response);
        }
    }
}
