using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Helpers
{
    public static class PaletteTerrainConditionHelper
    {
        private static IEnumerable<int> GetAllTerrainConditionValues(this Palette<ConditionPalettePoint> palette, BuildingType buildingType, TerrainType terrainType)
        {
            return palette.AllPoints.Select(a => a.TerrainConditions.GetValue(buildingType, terrainType));
        }

        public static int GetMaxTerrainConditionValue(this Palette<ConditionPalettePoint> palette, BuildingType buildingType, TerrainType terrainType)
        {
            var conditions = palette.GetAllTerrainConditionValues(buildingType, terrainType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Max();
        }

        public static int GetMinTerrainConditionValue(this Palette<ConditionPalettePoint> palette, BuildingType buildingType, TerrainType terrainType)
        {
            var conditions = palette.GetAllTerrainConditionValues(buildingType, terrainType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Min();
        }

        public static Range GetMinMaxTerrainConditionValue(this Palette<ConditionPalettePoint> palette, BuildingType buildingType, TerrainType terrainType)
        {
            return new Range
            {
                Bottom = palette.GetMinTerrainConditionValue(buildingType, terrainType),
                Top = palette.GetMaxTerrainConditionValue(buildingType, terrainType)
            };
        }
    }
}
