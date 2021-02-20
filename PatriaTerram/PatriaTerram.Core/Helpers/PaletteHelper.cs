﻿using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Helpers
{
    public static class PaletteHelper
    {
        public static int GetMaxBuildingConditionValue(this Palette palette, BuildingType buildingType, BuildingType terrainBuildingType)
        {
            var conditions = new List<int>();

            foreach (var point in palette.AllPoints)
            {
                if (point.BuildingConditions.IsHasCondition(buildingType, terrainBuildingType) == false) 
                { 
                    continue; 
                }

                conditions.Add(point.BuildingConditions.GetValue(buildingType, terrainBuildingType));
            }

            if (conditions.Count == 0)
            {
                return 0;
            }

            return conditions.Max();
        }

        public static int GetMaxTerrainConditionValue(this Palette palette, BuildingType buildingType, TerrainType terrainType)
        {
            var conditions = new List<int>();

            foreach (var point in palette.AllPoints)
            {
                if (point.TerrainConditions.IsHasCondition(buildingType, terrainType) == false)
                {
                    continue;
                }

                conditions.Add(point.TerrainConditions.GetValue(buildingType, terrainType));
            }

            if (conditions.Count == 0)
            {
                return 0;
            }

            return conditions.Max();
        }

        public static int GetMaxResultConditionValue(this Palette palette, BuildingType buildingType)
        {
            var conditions = new List<int>();

            foreach (var point in palette.AllPoints)
            {
                if (point.ResultConditions.IsHasCondition(buildingType) == false)
                {
                    continue;
                }

                conditions.Add(point.ResultConditions.GetValue(buildingType));
            }

            if (conditions.Count == 0)
            {
                return 0;
            }

            return conditions.Max();
        }

        public static Dictionary<int, List<Coord>> GetMaxBuildingConditionCoords(this Palette palette, BuildingType buildingType, BuildingType terrain)
        {
            var conditions = new Dictionary<int, List<Coord>>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    if (point.BuildingConditions.IsHasCondition(buildingType, terrain) == false) { continue; }

                    var value = point.BuildingConditions.GetValue(buildingType, terrain); //[buildingType].EnvironmentConditionValues[terrain];

                    if (conditions.Keys.Contains(value) == false)
                    {
                        conditions.Add(value, new List<Coord>());
                    }

                    conditions[value].Add(new Coord(x, y));
                }
            }

            return conditions;
        }

        public static Dictionary<int, List<Coord>> GetMaxBuildingConditionCoordsNew(this Palette palette, BuildingType buildingType, string terrain)
        {
            var conditions = new Dictionary<int, List<Coord>>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                   // if (point.ResultConditions.IsHasEnvironment(buildingType, terrain) == false) { continue; }

                    var value = point.ResultConditions.GetValue(buildingType); //[buildingType].EnvironmentConditionValues[terrain];

                    if (conditions.Keys.Contains(value) == false)
                    {
                        conditions.Add(value, new List<Coord>());
                    }

                    conditions[value].Add(new Coord(x, y));
                }
            }

            return conditions;
        }

        public static Coord GetMaxBuildingConditionCoord(this Palette palette, BuildingType buildingType, BuildingType terrain)
        {
            var coords = GetMaxBuildingConditionCoords(palette, buildingType, terrain);

            var maxValue = coords.Keys.Max();

            return coords[maxValue].FirstOrDefault();
        }

        public static Coord GetMaxBuildingConditionCoordWithoutBuildings(this Palette palette, BuildingType buildingType, string terrain)
        {
            var coords = GetMaxBuildingConditionCoordsNew(palette, buildingType, terrain);

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

                    row[newX] = palette[x,y];
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
    }
}
