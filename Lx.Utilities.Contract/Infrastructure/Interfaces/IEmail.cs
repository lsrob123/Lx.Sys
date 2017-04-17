namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IEmail : IVerified
    {
        string Address { get; }
    }
}