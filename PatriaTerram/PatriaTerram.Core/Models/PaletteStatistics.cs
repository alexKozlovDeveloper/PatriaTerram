﻿using PatriaTerram.Core.Configurations.Entityes;
using System.Collections.Generic;

namespace PatriaTerram.Core.Models
{
    public class PaletteStatistics
    {
        public Dictionary<string, Range> LayerMinMaxValues { get; private set; }

        public PaletteStatistics()
        {
            LayerMinMaxValues = new Dictionary<string, Range>();
        }

        public void AddLayerValue(string layerName, string descriptor, int value)
        {
            var key = $"{layerName}-{descriptor}";

            if(LayerMinMaxValues.ContainsKey(key) == false)
            {
                LayerMinMaxValues.Add(key, new Range { Top = value, Bottom = value });
            }
            else
            {
                var range = LayerMinMaxValues[key];

                if(range.Top < value)
                {
                    range.Top = value;
                }

                if (range.Bottom > value)
                {
                    range.Bottom = value;
                }
            }
        }
    }
}
