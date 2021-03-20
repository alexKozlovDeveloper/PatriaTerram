using PatriaTerram.Core.Configurations.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class PaletteStatistics
    {
        public List<PaletteStatisticsItem> LayerValueRanges { get; private set; }

        public Dictionary<string, Range> DescriptorValueRanges
        {
            get
            {
                return LayerValueRanges
                        .ToDictionary(a => a.Descriptor, a => a.ValueRange);
            }
        }

        public PaletteStatistics()
        {
            LayerValueRanges = new List<PaletteStatisticsItem>();
        }

        public void UpdateLayerItemValue(string layerName, string descriptor, int value)
        {
            var item = LayerValueRanges.FirstOrDefault(a => a.Layer == layerName && a.Descriptor == descriptor);

            if (item == null)
            {
                item = new PaletteStatisticsItem()
                {
                    Layer = layerName,
                    Descriptor = descriptor,
                    ValueRange = new Range()
                };

                LayerValueRanges.Add(item);
            }

            if (item.ValueRange.Top < value)
            {
                item.ValueRange.Top = value;
            }

            if (item.ValueRange.Bottom > value)
            {
                item.ValueRange.Bottom = value;
            }
        }

        public Dictionary<string, Range> GetLayerValueRanges(string layer)
        {
            return LayerValueRanges
                    .Where(a => a.Layer == layer)
                    .ToDictionary(a => a.Descriptor, a => a.ValueRange);
        }

        //TODO: remove
        public string GetReport()
        {
            var resutl = "";

            foreach (KeyValuePair<string, Range> item in DescriptorValueRanges)
            {
                resutl += $"{item.Key}-[{item.Value.Bottom}-{item.Value.Top}]" + Environment.NewLine;
            }

            return resutl;
        }
    }
}
