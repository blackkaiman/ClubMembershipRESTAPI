using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Models
{
    public class Membership
    {
        public Guid IdMembership { get; set; }
        public Guid IdMember { get; set; }
        public Guid IdMembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Level { get; set; }
    }
}
