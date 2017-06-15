using Lx.Utilities.Contracts.Persistence;

namespace Lx.Communications.Endpoint.Config
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString => "name=Communications";
    }
}