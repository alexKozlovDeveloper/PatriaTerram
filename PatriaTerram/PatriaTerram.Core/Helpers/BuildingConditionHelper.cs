using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class BuildingConditionHelper
    {
        public static void AddConditionValue(this BuildingCondition condition, string terrain, int value)
        {
            if (condition.EnvironmentConditionValues.Keys.Contains(terrain) == false)
            {
                condition.EnvironmentConditionValues.Add(terrain, 0);
            }

            condition.EnvironmentConditionValues[terrain] += value;
        }

        public static void UpdateConditionValue(this BuildingCondition condition, string terrain, int value)
        {
            if (condition.EnvironmentConditionValues.Keys.Contains(terrain) == false)
            {
                condition.EnvironmentConditionValues.Add(terrain, 0);
            }

            condition.EnvironmentConditionValues[terrain] = value;
        }
    }
}
