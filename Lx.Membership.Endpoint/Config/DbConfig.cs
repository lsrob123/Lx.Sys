using Lx.Utilities.Contracts.Persistence;

namespace Lx.Membership.Endpoint.Config
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString => "name=Membership";
    }
}