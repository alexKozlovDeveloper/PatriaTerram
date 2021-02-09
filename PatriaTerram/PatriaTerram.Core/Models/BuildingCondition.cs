using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class BuildingCondition
    {       
        public string BuildingType { get; set; }

        public Dictionary<string, int> TerrainConditionValues { get; }

        public BuildingCondition()
        {
            TerrainConditionValues = new Dictionary<string, int>();
        }
    }
}
