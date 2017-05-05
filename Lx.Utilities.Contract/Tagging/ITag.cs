using System;

namespace Lx.Utilities.Contract.Tagging {
    public interface ITag {
        string Scope { get; }
        Guid Key { get; }
        Guid OwnerObjectKey { get; }
        string Text { get; }
        TagType TagType { get; }
    }
}