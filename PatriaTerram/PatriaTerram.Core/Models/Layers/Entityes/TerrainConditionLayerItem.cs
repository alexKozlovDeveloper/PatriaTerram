using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Models.Layers.Entityes
{
    public class TerrainConditionLayerItem : ILayerItem
    {
        //public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
        public TerrainType EnvironmentTerrainType { get; set; }
        public int Value { get; set; }
    }
}
