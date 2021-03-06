﻿using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contracts.Infrastructure.Domain;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.ValueObjects
{
    public class Device : IValueObject, IDevice
    {
        public Device()
        {
        }

        public Device(string id, string operatingSystem = null, string operatingSystemVersion = null,
            string model = null)
        {
            ManufacturerId = id;
            OperatingSystem = operatingSystem;
            OperatingSystemVersion = operatingSystemVersion;
            Model = model;
        }

        [StringLength(100)]
        public string ManufacturerId { get; protected set; }

        [StringLength(100)]
        public string OperatingSystem { get; protected set; }

        [StringLength(100)]
        public string OperatingSystemVersion { get; protected set; }

        [StringLength(100)]
        public string Model { get; protected set; }
    }
}