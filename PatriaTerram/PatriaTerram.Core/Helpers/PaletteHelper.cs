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

                    if (point.BuildingConditions.Keys.Contains(buildingType) == false) { continue; }

                    if (point.BuildingConditions[buildingType].EnvironmentConditionValues.Keys.Contains(terrain) == false) { continue; }

                    conditions.Add(point.BuildingConditions[buildingType].EnvironmentConditionValues[terrain]);
                }
            }

            if (conditions.Count == 0)
            {
                return 0;
            }

            return conditions.Max();
        }

        public static Dictionary<int, List<Coord>> GetMaxBuildingConditionCoords(this Palette palette, string buildingType, string terrain)
        {
            var conditions = new Dictionary<int, List<Coord>>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    if (point.BuildingConditions.Keys.Contains(buildingType) == false) { continue; }

                    if (point.BuildingConditions[buildingType].EnvironmentConditionValues.Keys.Contains(terrain) == false) { continue; }

                    var value = point.BuildingConditions[buildingType].EnvironmentConditionValues[terrain];

                    if (conditions.Keys.Contains(value) == false)
                    {
                        conditions.Add(value, new List<Coord>());
                    }

                    conditions[value].Add(new Coord(x, y));
                }
            }

            return conditions;
        }

        public static Coord GetMaxBuildingConditionCoord(this Palette palette, string buildingType, string terrain)
        {
            var coords = GetMaxBuildingConditionCoords(palette, buildingType, terrain);

            var maxValue = coords.Keys.Max();

            return coords[maxValue].FirstOrDefault();
        }

        public static Coord GetMaxBuildingConditionCoordWithoutBuildings(this Palette palette, string buildingType, string terrain)
        {
            var coords = GetMaxBuildingConditionCoords(palette, buildingType, terrain);

            var sortedKeys = coords.Keys.OrderBy(a => a).Reverse();

            foreach (var key in sortedKeys)
            {
                foreach (var item in coords[key])
                {
                    if (palette[item].Buildings.Count == 0)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
}
