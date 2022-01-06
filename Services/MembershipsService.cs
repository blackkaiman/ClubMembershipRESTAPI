using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class MembershipsService : IMembershipsService
    {
        private readonly ClubMembershipDbContext _context;
        public MembershipsService(ClubMembershipDbContext context)
        {
            _context = context;
        }
        public void Delete(Membership membership)
        {
            _context.Remove(membership);
            _context.SaveChanges();
        }

        public DbSet<Membership> Get()
        {
            return _context.Memberships;
        }

        public void Post(Membership membership)
        {
            var _membership = new Membership()
            {
                IdMember = membership.IdMember,
                IdMembership = membership.IdMembership,
                IdMembershipType = membership.IdMembershipType,
                EndDate = membership.EndDate,
                StartDate = membership.StartDate,
                Level = membership.Level
            };
            _context.Entry(_membership).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Put(Membership membership)
        {
            _context.Update(membership);
            _context.SaveChanges();
        }
    }
}
