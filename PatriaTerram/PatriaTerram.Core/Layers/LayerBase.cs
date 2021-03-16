using PatriaTerram.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Layers
{
    public abstract class LayerBase<T> : ILayer where T : ILayerItem
    {
        public virtual string Name { get; }
        private List<T> Items { get; set; }

        public int ItemsCount { get { return Items.Count; } }

        public LayerBase()
        {
            Items = new List<T>();
        }

        public event ILayerHelper.AddItemHandler AddItemEvent;

        public IEnumerable<T> GetAll()
        {
            return Items;
        }

        public int GetMaxValue()
        {
            if (Items.Count == 0)
            {
                return 0;
            }

            return Items.Select(a => a.Value).Max();
        }

        public int GetMinValue()
        {
            if (Items.Count == 0)
            {
                return 0;
            }

            return Items.Select(a => a.Value).Min();
        }

        public void AddItem(T item)
        {
            Items.Add(item);

            AddItemEvent(Name, item.Descriptor, item.Value);
        }

        public T GetItem(Func<T, bool> func)
        {
            return Items.FirstOrDefault(a => func(a));
        }
    }
}
