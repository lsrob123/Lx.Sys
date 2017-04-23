using Lx.Utilities.Contract.Persistence;

namespace Lx.Identity.Endpoint.Config
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString => "name=Identity";
    }
}