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

            foreach (var terrainType in basePoint.Terrains.TerrainTypes)
            {
                var environmentCondition = building.EnvironmentConditions.FirstOrDefault(a => a.Environment == terrainType.ToString());

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

                    palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, terrainType.ToString(), value);
                }

            }
        }

        public void FinalResolve(Palette palette, Building building)
        {
            var maxConditions = GetMaxConditions(palette, building);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    if (palette[x, y].BuildingConditions.IsHasBuildingCondition(building.Type) == false) { continue; }

                    //var conditions = palette[x, y].BuildingConditions[building.Name];

                    double sum = 0;

                    foreach (var terrainCondition in building.EnvironmentConditions)
                    {
                        //if (conditions.EnvironmentConditionValues.Keys.Contains(terrainCondition.Environment) == false) { continue; }
                        if (palette[x, y].BuildingConditions.IsHasEnvironment(building.Type, terrainCondition.Environment) == false) { continue; }

                        //var conditionValue = conditions.EnvironmentConditionValues[terrainCondition.Environment];
                        var conditionValue = palette[x, y].BuildingConditions.GetValue(building.Type, terrainCondition.Environment);

                        //if (conditionValue == 0 && terrainCondition.IsRequired == true)
                        //{
                        //    sum = 0;
                        //    break;
                        //}

                        sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.Environment]) * terrainCondition.Priority;
                    }

                    sum /= building.EnvironmentConditions.Select(a => a.Priority).Sum();
                    sum *= 1000;

                    palette[x, y].BuildingConditions.UpdateValue(building.Type, Constants.Result, (int)sum);
                    //conditions.UpdateConditionValue(Constants.Result, (int)sum);
                }
            }
        }

        private Dictionary<string, int> GetMaxConditions(Palette palette, Building building)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var terrainCondition in building.EnvironmentConditions)
            {
                var value = Math.Abs(palette.GetMaxBuildingConditionValue(building.Type, terrainCondition.Environment));

                maxConditions.Add(terrainCondition.Environment, value);
            }

            return maxConditions;
        }

        public static void UpdateBuildingEffects(Palette palette, Coord baseCoord)
        {
            var basePoint = palette[baseCoord];

            foreach (var pointBuildingType in basePoint.Buildings.GetBuildings())
            {
                foreach (var building in Configs.Buildings.Values)
                {
                    var pointBuilding = Configs.Buildings[pointBuildingType];

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

                            palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, pointBuilding.Name, value);
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
