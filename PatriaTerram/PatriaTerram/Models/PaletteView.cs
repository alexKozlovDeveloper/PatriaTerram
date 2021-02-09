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
                context.MaxConditions.Add(terrain.Name, palette.GetMaxBuildingConditionValue(Constants.TownHall, terrain.Name));
            }

            context.MaxTerrainValue = GetMaxTerrainValue(palette);

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

                    foreach (var terrain in point.Terrains.Values)
                    {
                        if(terrain.Value > max)
                        {
                            max = terrain.Value;
                        }
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