using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class PaletteBuildingConditionHelper
    {
        private static IEnumerable<int> GetAllBuildingConditionValues(this Palette palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            return palette.AllPoints.Select(a => a.BuildingConditions.GetValue(townName, buildingType, terrainBuildingType));
        }

        public static int GetMaxBuildingConditionValue(this Palette palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            var conditions = palette.GetAllBuildingConditionValues(townName, buildingType, terrainBuildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Max();
        }

        public static int GetMinBuildingConditionValue(this Palette palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            var conditions = palette.GetAllBuildingConditionValues(townName, buildingType, terrainBuildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Min();
        }

        public static Range GetMinMaxBuildingConditionValue(this Palette palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            return new Range
            {
                Bottom = palette.GetMinBuildingConditionValue(townName, buildingType, terrainBuildingType),
                Top = palette.GetMaxBuildingConditionValue(townName, buildingType, terrainBuildingType)
            };
        }
    }
}
