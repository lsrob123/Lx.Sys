using System.Data.Entity.Migrations;
using System.Linq;
using Lx.Identity.Persistence.EF;
using Lx.Identity.Persistence.Seeding;
using Lx.Utilities.Services.Config;

namespace Lx.Identity.Persistence.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IdentityDbContext>
    {
        public Configuration()
        {
            Preconfigurator.ConfigureMapping();
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdentityDbContext context)
        {
            foreach (var user in UserSeedCollections.Users())
                if (!context.Users.Any(x => x.Key == user.Key))
                    context.Users.AddOrUpdate(user);

            foreach (var userProfile in UserSeedCollections.UserProfiles())
                if (!context.UserProfiles.Any(x => x.Key == userProfile.Key))
                    context.UserProfiles.AddOrUpdate(userProfile);

            foreach (var client in ClientSeedCollections.Clients())
                if (!context.Clients.Any(x => x.Key == client.Key))
                    context.Clients.AddOrUpdate(client);

            foreach (var clientScope in ClientSeedCollections.ClientScopes())
                if (!context.ClientScopes.Any(x => x.Key == clientScope.Key))
                    context.ClientScopes.AddOrUpdate(clientScope);

            foreach (var clientSecret in ClientSeedCollections.ClientSecrets())
                if (!context.ClientSecrets.Any(x => x.Key == clientSecret.Key))
                    context.ClientSecrets.AddOrUpdate(clientSecret);

            foreach (var scope in ScopeSeedCollections.Scopes())
                if (!context.Scopes.Any(x => x.Key == scope.Key))
                    context.Scopes.AddOrUpdate(scope);

            foreach (var scopeClaim in ScopeSeedCollections.ScopeClaims())
                if (!context.ScopeClaims.Any(x => x.Key == scopeClaim.Key))
                    context.ScopeClaims.AddOrUpdate(scopeClaim);

            foreach (var scopeSecret in ScopeSeedCollections.ScopeSecrets())
                if (!context.ScopeSecrets.Any(x => x.Key == scopeSecret.Key))
                    context.ScopeSecrets.AddOrUpdate(scopeSecret);
        }
    }
}