using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Models.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public Guid Username { get; set; } //idMember - tabela Member

        [Required]
        public string Password { get; set; } //name - tabela Member
    }
}