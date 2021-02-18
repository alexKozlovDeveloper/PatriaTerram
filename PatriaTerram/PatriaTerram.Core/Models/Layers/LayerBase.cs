using PatriaTerram.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers
{
    public abstract class LayerBase<T> : ILayer where T : ILayerItem
    {
        public virtual string Name { get; }
        protected List<T> Items { get; private set; }

        public LayerBase()
        {
            Items = new List<T>();
        }
    }
}
