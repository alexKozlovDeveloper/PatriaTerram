using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;

namespace PatriaTerram.Core.Helpers
{
    public static class PalettePointColorHelper
    {
        public static void GetPointColor(this PalettePoint point, out int r, out int g, out int b)
        {
            var coloredTerrains = new List<Terrain>();

            foreach (var terrainType in point.Terrains.GetTerrainTypes())
            {
                var terrain = Configs.Terrains[terrainType];

                if(terrain.IsAffectColor == true)
                {
                    coloredTerrains.Add(terrain);
                }
            }

            r = GetAvarageField(coloredTerrains, a => a.Color.R);
            g = GetAvarageField(coloredTerrains, a => a.Color.G);
            b = GetAvarageField(coloredTerrains, a => a.Color.B);
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
    }
}
