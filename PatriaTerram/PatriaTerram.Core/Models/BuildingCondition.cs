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

        public void AddConditionValue(string terrain, int value)
        {
            if (EnvironmentConditionValues.Keys.Contains(terrain) == false)
            {
                EnvironmentConditionValues.Add(terrain, 0);
            }

            EnvironmentConditionValues[terrain] += value;
        }

        public void UpdateConditionValue(string terrain, int value)
        {
            if (EnvironmentConditionValues.Keys.Contains(terrain) == false)
            {
                EnvironmentConditionValues.Add(terrain, 0);
            }

            EnvironmentConditionValues[terrain] = value;
        }
    }
}
