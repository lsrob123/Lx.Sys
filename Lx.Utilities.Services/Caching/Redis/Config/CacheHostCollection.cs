using System.Configuration;

namespace Lx.Utilities.Services.Caching.Redis.Config
{
    public class CacheHostCollection : ConfigurationElementCollection
    {
        public CacheHost this[int index]
        {
            get => BaseGet(index) as CacheHost;
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheHost();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheHost) element).Host;
        }
    }
}