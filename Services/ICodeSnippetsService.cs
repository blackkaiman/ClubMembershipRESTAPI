using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Services
{
    public interface ICodeSnippetsService
    {
        public DbSet<CodeSnippet> Get();
        public void Post(CodeSnippet codeSnippet);
        public void Put(CodeSnippet codeSnippet);
        public void Delete(CodeSnippet codeSnippet);
    }
}
