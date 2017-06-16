namespace Lx.Utilities.Contracts.Email
{
    public interface IEmailSettings
    {
        bool DumpToFilesOnly { get; }
        string DumpFileFolder { get; }
    }
}