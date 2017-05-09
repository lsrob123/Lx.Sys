using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lx.Utilities.Contract.Infrastructure.Domain {
    public abstract class EntityBase : IEntity {
        protected EntityBase() : this(Guid.NewGuid()) {
            TimeCreated = TimeCreated ?? DateTimeOffset.UtcNow;
            TimeModified = TimeModified ?? DateTimeOffset.UtcNow;
        }

        protected EntityBase(Guid key) {
            InitComplexProperties();
            Key = key;
        }

        public DateTimeOffset? TimeCreated { get; protected set; }
        public DateTimeOffset? TimeModified { get; protected set; }

        [Key]
        [IgnoreDataMember]
        public long Id { get; protected set; }

        public Guid Key { get; protected set; }

        public virtual void SetKey(Guid key) {
            Key = key;
        }

        public Guid EnsureValidKey() {
            if (Key == Guid.Empty)
                Key = Guid.NewGuid();
            return Key;
        }

        public virtual void SetId(long id) {
            Id = id;
        }

        public abstract void AssignDefaultValuesToComplexPropertiesIfNull();

        public void SetTimeCreated(DateTimeOffset? timeCreated) {
            TimeCreated = timeCreated;
        }

        public void SetTimeModified(DateTimeOffset? timeModified) {
            TimeModified = timeModified;
        }

        private void InitComplexProperties() {
            AssignDefaultValuesToComplexPropertiesIfNull();
        }
    }
}