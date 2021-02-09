using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class PalettePointHelper
    {
        public static void GetPointColor(this PalettePoint point, out int r, out int g, out int b)
        {
            var coloredComponents = point.Terrains.Where(a => a.Key.IsAffectColor);

            r = GetAvarageField(coloredComponents, a => a.Key.ColorR);
            g = GetAvarageField(coloredComponents, a => a.Key.ColorG);
            b = GetAvarageField(coloredComponents, a => a.Key.ColorB);
        }

        private static int GetAvarageField<T>(IEnumerable<T> items, Func<T, int> field)
        {
            var sum = 0;
            var count = 0;

            foreach (var item in items)
            {
                sum += field(item);
                count++;
            }

            if(count == 0) { return 0; }

            return sum / count;
        }

        public static void AddBuildingConditions(this PalettePoint point, string buildingType, string terrain, int value)
        {
            var existCondition = point.BuildingConditions.FirstOrDefault(a => a.BuildingType == buildingType);

            if (existCondition == null)
            {
                existCondition = new BuildingCondition();

                existCondition.BuildingType = buildingType;

                point.BuildingConditions.Add(existCondition);
            }

            existCondition.AddConditionValue(terrain, value);
        }

        public static int GetBuildingConditionValue(this PalettePoint point, string buildingType, string terrain)
        {
            var condition = point.BuildingConditions.FirstOrDefault(a => a.BuildingType == buildingType);

            if (condition == null) { return 0; }

            return condition.TerrainConditionValues[terrain];
        }

        public static int GetResultBuildingConditionValue(this PalettePoint point, string buildingType)
        {
            var condition = point.BuildingConditions.FirstOrDefault(a => a.BuildingType == buildingType);

            if (condition == null) { return 0; }

            //return (int)condition.TerrainConditionValues.Values.Average();
            return condition.TerrainConditionValues.Values.Sum();
        }
    }
}
