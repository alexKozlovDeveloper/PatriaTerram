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
            if (condition.TerrainConditionValues.Keys.Contains(terrain) == false)
            {
                condition.TerrainConditionValues.Add(terrain, 0);
            }

            condition.TerrainConditionValues[terrain] += value;
        }
    }
}
