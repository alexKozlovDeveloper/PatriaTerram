﻿using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Interfaces;

namespace PatriaTerram.Core.Condition.Layers.Entityes
{
    public class BuildingLayerItem : ILayerItem
    {
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}
