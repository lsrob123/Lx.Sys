using System;
using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Contract.Tagging {
    public abstract class TagBase : EntityBase, ITag {

        public abstract string Scope { get; }
        public Guid OwnerObjectKey { get; protected set; }

        [StringLength(100)]
        public string Text { get; protected set; }

        public TagType TagType { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {
            TagType = TagType ?? TagType.Unknown;
        }
    }
}