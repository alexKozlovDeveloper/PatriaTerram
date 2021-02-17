using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.BuildingConditions
{
    public class BuildingConditionsResolver
    {
        public void ResolvePoint(Palette palette, Coord baseCoord, Building building)
        {
            var basePoint = palette[baseCoord];

            foreach (var terrain in basePoint.Terrains.Keys)
            {
                var environmentCondition = building.EnvironmentConditions.FirstOrDefault(a => a.Environment == terrain);

                if (environmentCondition == null) { continue; }

                var radius = environmentCondition.Radius;

                var adjacentCoords = baseCoord.GetAdjacentCoordsBeyond(radius, palette.Width, palette.Height);

                foreach (var adjacentCoord in adjacentCoords)
                {
                    int value = 0;

                    if (environmentCondition.Type == EnvironmentConditionType.LinearDecrease)
                    {
                        value = GetValueLinearDecrease(baseCoord, adjacentCoord, radius, palette.Width, palette.Height);
                    }
                    else
                    {
                        value = GetValueOneLevel(baseCoord, adjacentCoord, radius, palette.Width, palette.Height);
                    }

                    if (environmentCondition.IsPositive == false)
                    {
                        value *= -1;
                    }

                    palette[adjacentCoord].AddBuildingConditions(building.Name, terrain, value);
                }

            }
        }

        public void FinalResolve(Palette palette, Building building)
        {
            var maxConditions = GetMAxConditions(palette, building);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    if (palette[x, y].BuildingConditions.Keys.Contains(building.Name) == false) { continue; }

                    var conditions = palette[x, y].BuildingConditions[building.Name];

                    double sum = 0;

                    foreach (var terrainCondition in building.EnvironmentConditions)
                    {
                        if (conditions.EnvironmentConditionValues.Keys.Contains(terrainCondition.Environment) == false) { continue; }

                        var conditionValue = conditions.EnvironmentConditionValues[terrainCondition.Environment];

                        sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.Environment]) * terrainCondition.Priority;
                    }

                    sum /= building.EnvironmentConditions.Select(a => a.Priority).Sum();
                    sum *= 1000;

                    conditions.UpdateConditionValue(Constants.Result, (int)sum);
                }
            }
        }

        private Dictionary<string, int> GetMAxConditions(Palette palette, Building building)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var terrainCondition in building.EnvironmentConditions)
            {
                var value = Math.Abs(palette.GetMaxBuildingConditionValue(building.Name, terrainCondition.Environment));

                maxConditions.Add(terrainCondition.Environment, value);
            }

            return maxConditions;
        }

        public static void UpdateBuildingEffects(Palette palette, Coord baseCoord)
        {
            var basePoint = palette[baseCoord];

            foreach (var pointBuilding in basePoint.Buildings.Values)
            {
                foreach (var building in Configs.Buildings.Values)
                {
                    var effectedBuildoings = building.EnvironmentConditions.Where(a => a.Environment == pointBuilding.Name);

                    foreach (var effectedBuildoing in effectedBuildoings)
                    {
                        var coords = baseCoord.GetAdjacentCoordsBeyond(effectedBuildoing.Radius, palette.Width, palette.Height);

                        foreach (var adjacentCoord in coords)
                        {
                            int value = 0;

                            if (effectedBuildoing.Type == EnvironmentConditionType.LinearDecrease)
                            {
                                value = GetValueLinearDecrease(baseCoord, adjacentCoord, effectedBuildoing.Radius, palette.Width, palette.Height);
                            }
                            else
                            {
                                value = GetValueOneLevel(baseCoord, adjacentCoord, effectedBuildoing.Radius, palette.Width, palette.Height);
                            }

                            if (effectedBuildoing.IsPositive == false)
                            {
                                value *= -1;
                            }

                            palette[adjacentCoord].AddBuildingConditions($"{building.Name}", pointBuilding.Name, value);
                        }
                    }
                }
            }
        }

        public static int GetValueLinearDecrease(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, width, height)) * (100.0 / radius));
        }

        public static int GetValueOneLevel(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return 100;
        }
    }
}
