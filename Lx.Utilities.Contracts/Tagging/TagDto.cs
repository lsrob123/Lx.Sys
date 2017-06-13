using System;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Tagging
{
    public class TagDto : IDto, ITag
    {
        public string Scope { get; set; }
        public Guid Key { get; set; }
        public Guid OwnerObjectKey { get; set; }
        public string Text { get; set; }
        public TagType TagType { get; set; }
    }
}