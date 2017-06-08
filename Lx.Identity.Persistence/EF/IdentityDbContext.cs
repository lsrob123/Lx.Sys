using System.Data.Entity;
using Lx.Identity.Domain.Entities;
using Lx.Utilities.Services.Persistence.EF;

namespace Lx.Identity.Persistence.EF
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext() : this("name=Identity")
        {
        }

        public IdentityDbContext(string connectionString) : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<ScopeClaim> ScopeClaims { get; set; }
        public DbSet<ScopeSecret> ScopeSecrets { get; set; }
        public DbSet<Consent> Consents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.UniquelyIndexEntityKey<User>();
            modelBuilder.Index<User>(x => x.Username, true);
            modelBuilder.Index<User>(x => x.Email.Address, true);
            modelBuilder.Index<User>(x => x.Mobile.LocalNumberWithAreaCodeInDigits, true);

            modelBuilder.UniquelyIndexEntityKey<Client>();
            modelBuilder.Index<Client>(x => x.ClientId, true);
            modelBuilder.Index<Client>(x => x.ClientName, true);
            modelBuilder.Index<User>(x => x.Mobile.LocalNumberWithAreaCodeInDigits, true);

            modelBuilder.UniquelyIndexEntityKey<Scope>();
            modelBuilder.Index<Scope>(x => x.Name, true);

            modelBuilder.UniquelyIndexEntityKey<UserProfile>();

            modelBuilder.UniquelyIndexEntityKey<ClientClaim>();
            modelBuilder.UniquelyIndexEntityKey<ClientCorsOrigin>();
            modelBuilder.UniquelyIndexEntityKey<ClientCustomGrantType>();
            modelBuilder.UniquelyIndexEntityKey<ClientIdPRestriction>();
            modelBuilder.UniquelyIndexEntityKey<ClientPostLogoutRedirectUri>();
            modelBuilder.UniquelyIndexEntityKey<ClientRedirectUri>();
            modelBuilder.UniquelyIndexEntityKey<ClientScope>();
            modelBuilder.UniquelyIndexEntityKey<ClientSecret>();
            modelBuilder.UniquelyIndexEntityKey<ScopeClaim>();
            modelBuilder.UniquelyIndexEntityKey<ScopeSecret>();
            modelBuilder.UniquelyIndexEntityKey<Consent>();

            base.OnModelCreating(modelBuilder);
        }
    }
}