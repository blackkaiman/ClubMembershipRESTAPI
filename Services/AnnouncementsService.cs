using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class IAnnouncementService : IAnnouncementsService
    {
        private readonly ClubMembershipDbContext _context;
        public IAnnouncementService(ClubMembershipDbContext context)
        {
            _context = context;

        }
        public void Delete(Announcement announcement)
        {
            _context.Remove(announcement);
            _context.SaveChanges();
        }

        public DbSet<Announcement> Get()
        {
            return _context.Announcements;
        }

        public void Post(Announcement announcement)
        {
            var _announcement = new Announcement()
            {
                IdAnnouncement = Guid.NewGuid(),
                Tags = announcement.Tags,
                Text = announcement.Text,
                EventDateTime = DateTime.Now,
                Title = announcement.Title,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now
            };
            _context.Entry(_announcement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Put(Announcement announcement)
        {
            _context.Update(announcement);
            _context.SaveChanges();
        }
    }
}
