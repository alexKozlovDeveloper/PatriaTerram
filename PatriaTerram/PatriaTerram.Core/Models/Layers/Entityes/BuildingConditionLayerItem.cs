using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers.Entityes
{
    public class BuildingConditionLayerItem : ILayerItem
    {
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
        public BuildingType EnvironmentBuildingType { get; set; }
        public int Value { get; set; }
        public BuildingCondition Condition { get; set; }

    }
}
