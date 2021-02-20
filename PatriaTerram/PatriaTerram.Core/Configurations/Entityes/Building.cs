using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Configurations.Entityes
{
    public class Building
    {
        public BuildingType Type { get; set; }

        public Color Color { get; set; }

        public int Value { get; set; }

        public List<BuildingCondition> BuildingConditions { get; set; }
        public List<TerrainCondition> TerrainConditions { get; set; }

        public Building() 
        {
            BuildingConditions = new List<BuildingCondition>();
            TerrainConditions = new List<TerrainCondition>();
        }
    }
}
