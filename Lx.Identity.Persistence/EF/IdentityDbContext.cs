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

        protected DbSet<User> Users { get; set; }
        protected DbSet<UserProfile> UserProfiles { get; set; }
        protected DbSet<Client> Clients { get; set; }
        protected DbSet<ClientClaim> ClientClaims { get; set; }
        protected DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        protected DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
        protected DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        protected DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        protected DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        protected DbSet<ClientScope> ClientScopes { get; set; }
        protected DbSet<ClientSecret> ClientSecrets { get; set; }
        protected DbSet<Scope> Scopes { get; set; }
        protected DbSet<ScopeClaim> ScopeClaims { get; set; }
        protected DbSet<ScopeSecret> ScopeSecrets { get; set; }
        protected DbSet<Consent> Consents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.UniquelyIndexEntityKey<User>();
            modelBuilder.Index<User>(x => x.Username, true);
            modelBuilder.Index<User>(x => x.Email.Address, true);
            modelBuilder.Index<User>(x => x.MobileNumber.LocalNumberWithAreaCodeInDigits, true);

            modelBuilder.UniquelyIndexEntityKey<Client>();
            modelBuilder.Index<Client>(x => x.ClientId, true);
            modelBuilder.Index<Client>(x => x.ClientName, true);
            modelBuilder.Index<User>(x => x.MobileNumber.LocalNumberWithAreaCodeInDigits, true);

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