using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public interface IMembershipsService
    {
        public DbSet<Membership> Get();
        public void Post(Membership membership);
        public void Put(Membership membership);
        public void Delete(Membership membership);
    }
}
