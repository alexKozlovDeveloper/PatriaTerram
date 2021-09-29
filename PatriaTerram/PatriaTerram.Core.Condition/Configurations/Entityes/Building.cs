using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using System.Collections.Generic;

namespace PatriaTerram.Core.Condition.Configurations.Entityes
{
    public class Building
    {
        public BuildingType Type { get; set; }

        public Color Color { get; set; }

        public int Value { get; set; }

        public List<BuildingCondition> BuildingConditions { get; set; }
        public List<TerrainCondition> TerrainConditions { get; set; }
        public TerrainType[] IntolerableTerrains { get; set; }

        public Building() 
        {
            BuildingConditions = new List<BuildingCondition>();
            TerrainConditions = new List<TerrainCondition>();
        }
    }
}
