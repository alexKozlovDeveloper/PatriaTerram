using PatriaTerram.Core;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using PatriaTerram.Web.Configurations;
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

        public PaletteView(Palette<ConditionPalettePoint> palette)
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

        private PaletteContext GetPaletteContext(Palette<ConditionPalettePoint> palette)
        {
            var context = new PaletteContext
            {
                TownNames = palette.GetAllTownNames(),
                TerrainTextureMaping = ImageConfigs.TerrainTextureMaping,
                BuildingTextureMaping = ImageConfigs.BuildingTextureMaping
            };

            foreach (var building in ConditionConfigs.Buildings.Values)
            {
                context.TownConditionRanges.Add(building.Type.ToString(), palette.GetConditionRanges(context.TownNames, building));
            }

            foreach (var terrain in Configs.Terrains.Values)
            {
                foreach (var building in ConditionConfigs.Buildings.Values)
                {
                    context.MaxConditions.Add($"{building.Type}-{terrain.Type}", palette.GetMaxTerrainConditionValue(building.Type, terrain.Type));
                }
            }

            foreach (var item1 in ConditionConfigs.Buildings.Values)
            {
                foreach (var item2 in ConditionConfigs.Buildings.Values)
                {
                    context.MaxConditions.Add($"{item1.Type}-{item2.Type}", palette.GetMaxBuildingConditionValue(context.TownNames.FirstOrDefault(), item1.Type, item2.Type));
                }
            }

            foreach (var building in ConditionConfigs.Buildings.Values)
            {
                var key = $"{building.Type}-Result";

                if (context.MaxConditions.ContainsKey(key) == true)
                {
                    context.MaxConditions[key] = palette.GetMaxResultConditionValue(context.TownNames.FirstOrDefault(), building.Type);
                }
                else
                {
                    context.MaxConditions.Add(key, palette.GetMaxResultConditionValue(context.TownNames.FirstOrDefault(), building.Type));
                }
            }

            context.MaxTerrainValue = GetMaxTerrainValue(palette);

            return context;
        }

        private int GetMaxTerrainValue(Palette<ConditionPalettePoint> palette)
        {
            var max = 0;

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    var maxPointTerrainValue = point.Terrains.GetMaxValue();

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