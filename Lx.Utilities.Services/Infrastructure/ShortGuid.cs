using System;

namespace Lx.Utilities.Services.Infrastructure
{
    public struct ShortGuid
    {
        public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

        public ShortGuid(string value)
        {
            Value = value;
            Guid = Decode(value);
        }

        public ShortGuid(Guid guid)
        {
            Value = Encode(guid);
            Guid = guid;
        }

        public Guid Guid { get; }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ShortGuid)
                return Guid.Equals(((ShortGuid) obj).Guid);
            if (obj is Guid)
                return Guid.Equals((Guid) obj);
            return false;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }

        public static string Encode(string value)
        {
            var guid = new Guid(value);
            return Encode(guid);
        }

        public static string Encode(Guid guid)
        {
            var encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");
            var buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }

        public static bool operator ==(ShortGuid x, ShortGuid y)
        {
            return x.Guid == y.Guid;
        }

        public static bool operator !=(ShortGuid x, ShortGuid y)
        {
            return !(x == y);
        }

        public static implicit operator string(ShortGuid shortGuid)
        {
            return shortGuid.Value;
        }

        public static implicit operator Guid(ShortGuid shortGuid)
        {
            return shortGuid.Guid;
        }

        public static implicit operator ShortGuid(string shortGuid)
        {
            return new ShortGuid(shortGuid);
        }

        public static implicit operator ShortGuid(Guid guid)
        {
            return new ShortGuid(guid);
        }
    }
}