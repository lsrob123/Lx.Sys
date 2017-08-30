namespace Lx.Utilities.Contract.Email
{
    public interface ISmtpSettings
    {
        string Username { get; }
        string Password { get; }
        string Host { get; }
        int Port { get; }
        bool IsSsl { get; }
        bool IsRealSend { get; }
    }
}