﻿namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IBasicRequestKey
    {
        string OriginatorGroup { get; set; }
        string RequestReference { get; set; }
        string OriginatorConnection { get; set; }
    }
}