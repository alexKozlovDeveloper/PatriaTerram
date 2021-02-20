using PatriaTerram.Core;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class PaletteView
    {
        public PalettePointView[][] Points { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public PaletteContext Context { get; set; }

        public PaletteView(Palette palette)
        {
            Width = palette.Width;
            Height = palette.Height;

            Points = GetPoints(Width, Height);

            Context = GetPaletteContext(palette);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    Points[x][y] = new PalettePointView(palette[x, y], Context);
                }
            }
        }

        private PaletteContext GetPaletteContext(Palette palette)
        {
            var context = new PaletteContext();

            foreach (var terrain in Configs.Terrains.Values)
            {
                foreach (var building in Configs.Buildings.Values)
                {
                    context.MaxConditions.Add($"{building.Type}-{terrain.Type}", palette.GetMaxTerrainConditionValue(building.Type, terrain.Type));
                }
            }

            foreach (var item1 in Configs.Buildings.Values)
            {
                foreach (var item2 in Configs.Buildings.Values)
                {
                    context.MaxConditions.Add($"{item1.Type}-{item2.Type}", palette.GetMaxBuildingConditionValue(item1.Type, item2.Type));
                }
            }

            foreach (var building in Configs.Buildings.Values)
            {
                var key = $"{building.Type}-Result";

                if (context.MaxConditions.ContainsKey(key) == true)
                {
                    context.MaxConditions[key] = palette.GetMaxResultConditionValue(building.Type);
                }
                else
                {
                    context.MaxConditions.Add(key, palette.GetMaxResultConditionValue(building.Type));
                }
            }

            context.MaxTerrainValue = GetMaxTerrainValue(palette);

            foreach (var point in palette.AllPoints)
            {
                foreach (var building in point.Buildings.GetAll())
                {
                    if(context.TownNames.Contains(building.TownName) == false)
                    {
                        context.TownNames.Add(building.TownName);
                    }
                }
            }

            return context;
        }

        private int GetMaxTerrainValue(Palette palette)
        {
            var max = 0;

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    var maxPointTerrainValue = point.Terrains.GetMaxTerrainValue();

                    if (maxPointTerrainValue > max)
                    {
                        max = maxPointTerrainValue;
                    }
                }
            }

            return max;
        }

        private PalettePointView[][] GetPoints(int width, int height)
        {
            var result = new PalettePointView[width][];

            for (int i = 0; i < width; i++)
            {
                result[i] = new PalettePointView[height];
            }

            return result;
        }
    }
}