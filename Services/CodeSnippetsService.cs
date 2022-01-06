using Microsoft.EntityFrameworkCore;
using ProiectPractica.App_Data;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public class CodeSnippetsService : ICodeSnippetsService
    {
        private readonly ClubMembershipDbContext _context;
        public CodeSnippetsService(ClubMembershipDbContext context)
        {
            _context = context;
        }
        public void Delete(CodeSnippet codeSnippet)
        {
            _context.Remove(codeSnippet);
            _context.SaveChanges();
        }

        public DbSet<CodeSnippet> Get()
        {
            return _context.CodeSnippets;
        }

        public void Post(CodeSnippet codeSnippet)
        {
            var codeS = new CodeSnippet()
            {
                IdCodeSnippet = Guid.NewGuid(),
                Title = codeSnippet.Title,
                ContentCode = codeSnippet.ContentCode,
                IdMember = codeSnippet.IdMember,
                Revision = codeSnippet.Revision,
                IsPublished = codeSnippet.IsPublished,
                DateTimeAdded = DateTime.Now
            };
            _context.Entry(codeS).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Put(CodeSnippet codeSnippet)
        {
            _context.Update(codeSnippet);
            _context.SaveChanges();
        }
    }
}
