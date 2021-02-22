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
    public static class PaletteResultConditionHelper
    {
        private static IEnumerable<int> GetAllResultConditionValues(this Palette palette, string townName, BuildingType buildingType)
        {
            return palette.AllPoints.Select(a => a.ResultConditions.GetValue(townName, buildingType));
        }

        public static int GetMaxResultConditionValue(this Palette palette, string townName, BuildingType buildingType)
        {
            var conditions = palette.GetAllResultConditionValues(townName, buildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Max();
        }

        public static int GetMinResultConditionValue(this Palette palette, string townName, BuildingType buildingType)
        {
            var conditions = palette.GetAllResultConditionValues(townName, buildingType);

            if (conditions.Count() == 0) { return 0; }

            return conditions.Min();
        }

        public static Range GetMinMaxResultConditionValue(this Palette palette, string townName, BuildingType buildingType)
        {
            return new Range
            {
                Bottom = palette.GetMinResultConditionValue(townName, buildingType),
                Top = palette.GetMaxResultConditionValue(townName, buildingType)
            };
        }
    }
}
