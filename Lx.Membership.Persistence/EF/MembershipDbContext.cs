using System.Data.Entity;
using Lx.Membership.Domain.Entities;
using Lx.Utilities.Contracts.Membership.Entities;

namespace Lx.Membership.Persistence.EF
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext() : this("name=Membership")
        {
        }

        public MembershipDbContext(string connectionString) : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleProcess> RoleProcesses { get; set; }
    }
}