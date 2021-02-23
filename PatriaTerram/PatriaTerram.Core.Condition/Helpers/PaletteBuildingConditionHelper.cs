using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Helpers
{
    public static class PaletteBuildingConditionHelper
    {
        private static IEnumerable<int> GetAllBuildingConditionValues(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            return palette.AllPoints.Select(a => a.BuildingConditions.GetValue(townName, buildingType, terrainBuildingType));
        }

        public static int GetMaxBuildingConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            var conditions = palette.GetAllBuildingConditionValues(townName, buildingType, terrainBuildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Max();
        }

        public static int GetMinBuildingConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            var conditions = palette.GetAllBuildingConditionValues(townName, buildingType, terrainBuildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Min();
        }

        public static Range GetMinMaxBuildingConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            return new Range
            {
                Bottom = palette.GetMinBuildingConditionValue(townName, buildingType, terrainBuildingType),
                Top = palette.GetMaxBuildingConditionValue(townName, buildingType, terrainBuildingType)
            };
        }
    }
}
