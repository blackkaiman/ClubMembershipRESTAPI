using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public interface IAnnouncementsService
    {
        public DbSet<Announcement> Get();
        public void Post(Announcement announcement);
        public void Put(Announcement announcement);
        public void Delete(Announcement announcement);
    }
}
