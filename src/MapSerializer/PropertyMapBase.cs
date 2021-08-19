using System.Reflection;

namespace MapSerializer
{
    public abstract class PropertyMapBase
    {
        internal PropertyInfo PropertyInfo { get; }

        public PropertyMapBase(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
        }
    }
}
