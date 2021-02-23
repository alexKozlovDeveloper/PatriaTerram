using PatriaTerram.Core.Interfaces;
using System.Collections.Generic;

namespace PatriaTerram.Core.Layers
{
    public abstract class LayerBase<T> : ILayer where T : ILayerItem
    {
        public virtual string Name { get; }
        protected List<T> Items { get; private set; }

        public LayerBase()
        {
            Items = new List<T>();
        }

        public List<T> GetAll()
        {
            return Items;
        }
    }
}
