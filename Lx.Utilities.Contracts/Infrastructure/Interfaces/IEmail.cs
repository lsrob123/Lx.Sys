namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IEmail : IVerified
    {
        string Address { get; }
    }
}