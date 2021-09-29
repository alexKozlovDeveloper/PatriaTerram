﻿using PatriaTerram.Core.Configurations.Entityes;

namespace PatriaTerram.Core.Models
{
    public class PaletteStatisticsItem
    {
        public string Layer { get; set; }
        public string Descriptor { get; set; }
        public Range ValueRange { get; set; }

        public override string ToString()
        {
            return $"({Layer}) {Descriptor} [{ValueRange.Bottom}-{ValueRange.Top}]";
        }
    }
}