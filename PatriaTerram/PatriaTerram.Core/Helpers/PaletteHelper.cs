using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class PaletteHelper
    {
        public static int GetMaxBuildingConditionValue(this Palette palette, string buildingType, string terrain)
        {
            var conditions = new List<int>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    var condition = point.BuildingConditions.FirstOrDefault(a => a.BuildingType == buildingType);

                    if(condition == null) { continue; }

                    if(condition.TerrainConditionValues.Keys.Contains(terrain) == false) { continue; }

                    conditions.Add(condition.TerrainConditionValues[terrain]);
                }
            }

            return conditions.Max();
        }
    }
}
