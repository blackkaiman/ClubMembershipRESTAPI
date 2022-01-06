using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public interface IMembersService
    {
        public DbSet<Member> Get();
        public void Post(Member member);
        public void Put(Member member);
        public void Delete(Member member);
    }
}
