using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectPractica.Models
{
    public class CodeSnippet
    {   [Key]
        public Guid IdCodeSnippet { get; set; }
        public string Title { get; set; }
        public string ContentCode { get; set; }
        public Guid IdMember { get; set; }
        public int Revision { get; set; }
        public bool IsPublished { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}
