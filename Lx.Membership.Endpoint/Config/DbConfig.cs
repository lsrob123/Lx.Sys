using Lx.Utilities.Contract.Persistence;

namespace Lx.Membership.Endpoint.Config
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString => "name=Membership";
    }
}