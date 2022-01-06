using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public interface IMembershipTypesService
    {
        public DbSet<MembershipType> Get();
        public void Post(MembershipType membershipType);
        public void Put(MembershipType membershipType);
        public void Delete(MembershipType membershipType);
    }
}
