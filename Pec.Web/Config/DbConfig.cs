using Lx.Utilities.Contracts.Persistence;

namespace Pec.Web.Config
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString => "name=Membership";
    }
}