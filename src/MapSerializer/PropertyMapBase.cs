using System.Reflection;

namespace MapSerializer
{
    internal abstract class PropertyMapBase
    {
        internal PropertyInfo PropertyInfo { get; }

        public PropertyMapBase(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
        }
    }
}
