using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Condition.Layers.Entityes
{
    public class BuildingConditionLayerItem : ILayerItem
    {
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
        public BuildingType EnvironmentBuildingType { get; set; }
        public int Value { get; set; }
        public BuildingCondition Condition { get; set; }

        public string Descriptor { get { return $"{TownName}-{BuildingType}-{EnvironmentBuildingType}"; } }
    }
}
