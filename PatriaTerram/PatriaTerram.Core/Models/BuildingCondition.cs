using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class BuildingCondition
    {       
        public string BuildingType { get; set; }

        public Dictionary<string, int> EnvironmentConditionValues { get; }

        public BuildingCondition()
        {
            EnvironmentConditionValues = new Dictionary<string, int>();
        }
    }
}
