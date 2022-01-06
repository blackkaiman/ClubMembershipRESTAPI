using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class MembershipTypesService : IMembershipTypesService
    {
        private readonly ClubMembershipDbContext _context;
        public MembershipTypesService(ClubMembershipDbContext context)
        {
            _context = context;
        }
        public void Delete(MembershipType membershipType)
        {
            _context.Remove(membershipType);
            _context.SaveChanges();
        }

        public DbSet<MembershipType> Get()
        {
            return _context.MembershipTypes;
        }

        public void Post(MembershipType membershipType)
        {
            var _membershipType = new MembershipType()
            {
                IdMembershipType = Guid.NewGuid(),
                Description = membershipType.Description,
                Name = membershipType.Name,
                SubscriptionLengthinMonths = membershipType.SubscriptionLengthinMonths
            };
            _context.Entry(_membershipType).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Put(MembershipType membershipType)
        {
            _context.Update(membershipType);
            _context.SaveChanges();

        }
    }
}
