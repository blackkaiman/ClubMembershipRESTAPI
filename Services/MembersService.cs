using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class MembersService : IMembersService
    {
        private readonly ClubMembershipDbContext _context;
        public MembersService(ClubMembershipDbContext context)
        {
            _context = context;
        }
        public void Delete(Member member)
        {
            _context.Remove(member);
            _context.SaveChanges();
        }

        public DbSet<Member> Get()
        {
            return _context.Members;
        }

        public void Post(Member member)
        {
            var _member = new Member()
            {
                IdMember = Guid.NewGuid(),
                Title = member.Title,
                Name = member.Name,
                Position = member.Position,
                Description = member.Description,
                Resume = member.Resume
            };
            _context.Entry(_member).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Put(Member member)
        {
            _context.Update(member);
            _context.SaveChanges();
        }
    }
}
