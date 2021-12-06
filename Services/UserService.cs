using Microsoft.IdentityModel.Tokens;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using ProiectPractica.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class UserService : IUserService
    {
        private readonly ClubMembershipDbContext _context;

        public UserService(ClubMembershipDbContext context)
        {
            _context = context;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Members.SingleOrDefault(x => x.IdMember == model.Username && x.Name == model.Password);
            if (user == null) return null;

            var token = GenerateJWTToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string GenerateJWTToken(Member user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AUTHSECRET");
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("IdMember", user.IdMember.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}
