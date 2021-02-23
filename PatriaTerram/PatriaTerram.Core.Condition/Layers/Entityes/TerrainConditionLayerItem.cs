using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Condition.Layers.Entityes
{
    public class TerrainConditionLayerItem : ILayerItem
    {
        public BuildingType BuildingType { get; set; }
        public TerrainType EnvironmentTerrainType { get; set; }
        public int Value { get; set; }
        public TerrainCondition Condition { get; set; }
    }
}
