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
            var coloredComponents = point.Terrains.Values.Where(a => a.Terrain.IsAffectColor);

            r = GetAvarageField(coloredComponents, a => a.Terrain.Color.R);
            g = GetAvarageField(coloredComponents, a => a.Terrain.Color.G);
            b = GetAvarageField(coloredComponents, a => a.Terrain.Color.B);
        }

        public static Color GetPointColor(this PalettePoint point)
        {
            point.GetPointColor(out int r, out int g, out int b);

            return new Color
            {
                R = r,
                G = g,
                B = b
            };
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





        //public static int GetResultBuildingConditionValue(this PalettePoint point, string buildingType)
        //{
        //    if (point.BuildingConditions.Keys.Contains(buildingType) == false) { return 0; }

        //    return point.BuildingConditions[buildingType].EnvironmentConditionValues.Values.Sum();
        //}
    }
}
