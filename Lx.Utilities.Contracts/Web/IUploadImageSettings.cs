﻿namespace Lx.Utilities.Contracts.Web
{
    public interface IUploadImageSettings : IUploadFileSettings
    {
        string BaseUri { get; }
        string RelativeUriBase { get; }
    }
}