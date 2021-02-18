using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers.Entityes
{
    public class TerrainLayerItem : ILayerItem
    {
        public TerrainType TerrainType { get; set; }
        public int Value { get; set; }
    }
}
