using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class AnnouncementsService : IAnnouncementService
    {
        private readonly ClubMembershipDbContext _context;
        public AnnouncementsService(ClubMembershipDbContext context)
        {
            _context = context;

        }
        public void Delete(Announcement announcement)
        {
            _context.Remove(announcement);
            _context.
        }

        public DbSet<Announcement> Get()
        {
            throw new NotImplementedException();
        }

        public void Post(Announcement announcement)
        {
            throw new NotImplementedException();
        }

        public void Put(Announcement announcement)
        {
            throw new NotImplementedException();
        }
    }
}
