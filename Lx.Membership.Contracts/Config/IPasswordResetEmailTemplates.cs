namespace Lx.Membership.Contracts.Config
{
    public interface IPasswordResetEmailTemplates
    {
        string Url { get; }
        string Subject { get; }
        string Body { get; }
    }
}