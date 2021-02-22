using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Helpers
{
    public static class PaletteHelper
    {
        public static Dictionary<int, List<Coord>> GetMaxBuildingConditionCoords(this Palette palette, string townName, BuildingType buildingType, string terrain)
        {
            var conditions = new Dictionary<int, List<Coord>>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    var value = point.ResultConditions.GetValue(townName, buildingType);

                    if (conditions.Keys.Contains(value) == false)
                    {
                        conditions.Add(value, new List<Coord>());
                    }

                    conditions[value].Add(new Coord(x, y));
                }
            }

            return conditions;
        }

        public static Coord GetMaxBuildingConditionCoordWithoutBuildings(this Palette palette, string townName, BuildingType buildingType, string terrain)
        {
            var coords = GetMaxBuildingConditionCoords(palette, townName, buildingType, terrain);

            var sortedKeys = coords.Keys.OrderBy(a => a).Reverse();

            foreach (var key in sortedKeys)
            {
                foreach (var item in coords[key])
                {
                    if (palette[item].Buildings.IsHasAnyBuildings() == false)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public static void MovePointsXAxis(this Palette palette, int distance)
        {
            for (int y = 0; y < palette.Height; y++)
            {
                var row = new PalettePoint[palette.Width];

                for (int x = 0; x < palette.Width; x++)
                {
                    var newX = x + distance;

                    if (newX >= palette.Width)
                    {
                        newX -= palette.Width;
                    }

                    if (newX < 0)
                    {
                        newX += palette.Width;
                    }

                    row[newX] = palette[x, y];
                }

                for (int x = 0; x < palette.Width; x++)
                {
                    palette[x, y] = row[x];
                }
            }
        }

        public static void MovePointsYAxis(this Palette palette, int distance)
        {
            for (int x = 0; x < palette.Width; x++)
            {
                var column = new PalettePoint[palette.Height];

                for (int y = 0; y < palette.Height; y++)
                {
                    var newY = y + distance;

                    if (newY >= palette.Height)
                    {
                        newY -= palette.Height;
                    }

                    if (newY < 0)
                    {
                        newY += palette.Height;
                    }

                    column[newY] = palette[x, y];
                }

                for (int y = 0; y < palette.Height; y++)
                {
                    palette[x, y] = column[y];
                }
            }
        }

        public static List<string> GetAllTownNames(this Palette palette)
        {
            var townNames = new List<string>();

            foreach (var point in palette.AllPoints)
            {
                foreach (var building in point.Buildings.GetAll())
                {
                    if (townNames.Contains(building.TownName) == false)
                    {
                        townNames.Add(building.TownName);
                    }
                }
            }

            return townNames;
        }

        public static Dictionary<string, Range> GetConditionRanges(this Palette palette, List<string> allTownNames, Building building)
        {
            var conditionRanges = new Dictionary<string, Range>();

            foreach (var buildingCondition in building.BuildingConditions)
            {
                var max = allTownNames.Select(townName => palette.GetMaxBuildingConditionValue(townName, building.Type, buildingCondition.EnvironmentBuilding))
                                      .Max();

                var min = allTownNames.Select(townName => palette.GetMinBuildingConditionValue(townName, building.Type, buildingCondition.EnvironmentBuilding))
                                      .Min();

                conditionRanges.Add(buildingCondition.EnvironmentBuilding.ToString(), new Range { Top = max, Bottom = min });
            }

            foreach (var terrainCondition in building.TerrainConditions)
            {
                var max = palette.GetMaxTerrainConditionValue(building.Type, terrainCondition.EnvironmentTerrain);
                var min = palette.GetMinTerrainConditionValue(building.Type, terrainCondition.EnvironmentTerrain);

                conditionRanges.Add(terrainCondition.EnvironmentTerrain.ToString(), new Range { Top = max, Bottom = min });
            }

            return conditionRanges;
        }
    }
}
