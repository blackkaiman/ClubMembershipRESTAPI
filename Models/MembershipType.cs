﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectPractica.Models
{
    public class MembershipType
    {
        public Guid IdMembershipType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubscriptionLengthinMonths { get; set; }
    }
}
