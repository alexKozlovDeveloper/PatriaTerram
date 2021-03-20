using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Condition.Layers.Entityes
{
    public class ResultConditionLayerItem : ILayerItem
    {
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
        public int Value { get; set; }

        public string Descriptor { get { return $"{TownName}-{BuildingType}-{Constants.Result}"; } }
    }
}
