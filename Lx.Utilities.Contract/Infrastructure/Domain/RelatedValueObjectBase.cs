﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.Domain {
    public abstract class RelatedValueObjectBase : IValueObject, IWithRelationalId {
        protected RelatedValueObjectBase() {
            TimeCreated = TimeCreated ?? DateTimeOffset.UtcNow;
            TimeModified = TimeModified ?? DateTimeOffset.UtcNow;
        }

        public DateTimeOffset? TimeCreated { get; protected set; }
        public DateTimeOffset? TimeModified { get; protected set; }

        [Key]
        [IgnoreDataMember]
        public long Id { get; protected set; }

        public virtual void SetId(long id) {
            Id = id;
        }

        public void SetTimeCreated(DateTimeOffset? timeCreated) {
            TimeCreated = timeCreated;
        }

        public void SetTimeModified(DateTimeOffset? timeModified) {
            TimeModified = timeModified;
        }
    }
}