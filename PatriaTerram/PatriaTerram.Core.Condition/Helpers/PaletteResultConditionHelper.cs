using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Helpers
{
    public static class PaletteResultConditionHelper
    {
        private static IEnumerable<int> GetAllResultConditionValues(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType)
        {
            return palette.AllPoints.Select(a => a.ResultConditions.GetValue(townName, buildingType));
        }

        public static int GetMaxResultConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType)
        {
            var conditions = palette.GetAllResultConditionValues(townName, buildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Max();
        }

        public static int GetMinResultConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType)
        {
            var conditions = palette.GetAllResultConditionValues(townName, buildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Min();
        }

        public static Range GetMinMaxResultConditionValue(this Palette<ConditionPalettePoint> palette, string townName, BuildingType buildingType)
        {
            return new Range
            {
                Bottom = palette.GetMinResultConditionValue(townName, buildingType),
                Top = palette.GetMaxResultConditionValue(townName, buildingType)
            };
        }
    }
}
