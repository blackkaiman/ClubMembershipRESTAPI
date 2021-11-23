using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Models
{
    public class CodeSnippet
    {
        public Guid IdCodeSnippet { get; set; }
        public string Title { get; set; }
        public string ContentCode { get; set; }
        public Guid IdMember { get; set; }
        public int Revision { get; set; }
        public bool IsPublished { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}
