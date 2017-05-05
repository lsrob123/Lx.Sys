using System;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Tagging {
    public class TagDto : IDto, ITag {
        public string Scope { get; set; }
        public Guid Key { get; set; }
        public Guid OwnerObjectKey { get; set; }
        public string Text { get; set; }
        public TagType TagType { get; set; }
    }
}