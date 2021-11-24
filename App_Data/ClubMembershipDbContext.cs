using Microsoft.EntityFrameworkCore;
using ProiectPractica.Models;

namespace ProiectPractica.App_Data
{
    public class ClubMembershipDbContext : DbContext
    {
        public ClubMembershipDbContext(DbContextOptions<ClubMembershipDbContext> options)
            :base(options)
        { }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CodeSnippet> CodeSnippets { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set;}

    }
}
