namespace Lx.Utilities.Contract.Web
{
    public interface IUploadImageSettings : IUploadFileSettings
    {
        string BaseUri { get; }
        string RelativeUriBase { get; }
    }
}