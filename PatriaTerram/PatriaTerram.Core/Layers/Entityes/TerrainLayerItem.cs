using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Layers.Entityes
{
    public class TerrainLayerItem : ILayerItem
    {
        public TerrainType TerrainType { get; set; }
        public int Value { get; set; }
    }
}
