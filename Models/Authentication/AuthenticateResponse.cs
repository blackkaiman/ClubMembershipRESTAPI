using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Models.Authentication
{
    public class AuthenticateResponse
    {
        public Guid IdMember { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Token { get; set; }
        public AuthenticateResponse(Member user, string token)
        {
            IdMember = user.IdMember;
            Name = user.Name;
            Title = user.Title;
            Token = token;
        }
    }
}
