using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using ProiectPractica.Services;
using System;
using System.Text.Json;

namespace ProiectPractica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CodeSnippetsController : ControllerBase
    {
        private readonly ILogger<CodeSnippetsController> _logger;
        private readonly ICodeSnippetsService _codeSnippetService;
        public CodeSnippetsController(ILogger<CodeSnippetsController> logger, ICodeSnippetsService codeSnippetService)
        {
            _logger = logger;
            _codeSnippetService = codeSnippetService;
        }

        [HttpGet]
        public IActionResult Get() //citeste date din tabel
        {
            if(_codeSnippetService != null)
            {
                return StatusCode(200, _codeSnippetService.Get());
            }
            else
            {
                return StatusCode(400, "No code snippets were found!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodeSnippet codeSnippet) //adauga inregistrare in tabel
        {
            try
            {
                _codeSnippetService.Post(codeSnippet);
                return StatusCode(201, "Code snippet was added in database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] CodeSnippet codeSnippet) //updateaza inregistrare in tabel
        {
            try
            {
                _codeSnippetService.Put(codeSnippet);
                return StatusCode(204, "Code snippet was updated from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] CodeSnippet codeSnippet) //sterge inregistrare in tabel
        {
            try
            {
                _codeSnippetService.Delete(codeSnippet);
                return StatusCode(204, "Code snippet was removed from swagger");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
