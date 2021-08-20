using System;
using System.Collections.Generic;

namespace MapSerializer
{
    internal abstract class TypeMapBase
    {
        public Type Type { get; protected set; }
        internal List<PropertyMapBase> MappedProperties { get; }

        public TypeMapBase()
        {
            this.MappedProperties = new List<PropertyMapBase>();
        }
    }
}
