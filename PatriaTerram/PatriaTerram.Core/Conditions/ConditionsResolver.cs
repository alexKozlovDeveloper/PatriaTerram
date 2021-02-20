using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Conditions
{
    public class ConditionsResolver
    {
        private Palette _palette;

        public ConditionsResolver(Palette palette)
        {
            _palette = palette;
        }

        public void ResolveTerrainCondition(Coord baseCoord, Building building)
        {
            var basePoint = _palette[baseCoord];

            foreach (var terrainType in basePoint.Terrains.GetTerrainTypes())
            {
                var environmentCondition = building.TerrainConditions.FirstOrDefault(a => a.EnvironmentTerrain == terrainType);

                if (environmentCondition == null) { continue; }

                var radius = environmentCondition.Radius;
                var adjacentCoords = baseCoord.GetAdjacentCoordsBeyond(radius, _palette.Width, _palette.Height);

                foreach (var adjacentCoord in adjacentCoords)
                {
                    var value = GetConditionValue(environmentCondition, baseCoord, adjacentCoord);

                    _palette[adjacentCoord].TerrainConditions.AddConditionValue(building.Type, terrainType, value);
                }
            }

        }

        public void ResolveBuildingCondition(Coord baseCoord, Building building)
        {
            var basePoint = _palette[baseCoord];

            foreach (var buildingLayerItem in basePoint.Buildings.GetAll())
            {
                var environmentCondition = building.BuildingConditions.FirstOrDefault(a => a.EnvironmentBuilding == buildingLayerItem.BuildingType);

                if (environmentCondition == null) { continue; }

                var radius = environmentCondition.Radius;
                var adjacentCoords = baseCoord.GetAdjacentCoordsBeyond(radius, _palette.Width, _palette.Height);

                foreach (var adjacentCoord in adjacentCoords)
                {
                    var value = GetConditionValue(environmentCondition, baseCoord, adjacentCoord);

                    _palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, environmentCondition.EnvironmentBuilding, value);
                }
            }
        }

        public void ResolveResultCondition(PalettePoint point, Building building, Dictionary<string, int> maxConditions)
        {
            double sum = 0;

            foreach (var terrainCondition in building.TerrainConditions)
            {
                var conditionValue = point.TerrainConditions.GetValue(building.Type, terrainCondition.EnvironmentTerrain);

                if (terrainCondition.IsRequired == true && conditionValue <= 0)
                {
                    point.ResultConditions.UpdateValue(building.Type, 0);
                    return;
                }

                sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.EnvironmentTerrain.ToString()]) * terrainCondition.Priority;
            }

            foreach (var buildingCondition in building.BuildingConditions)
            {
                var conditionValue = point.BuildingConditions.GetValue(building.Type, buildingCondition.EnvironmentBuilding);

                if (buildingCondition.IsRequired == true && conditionValue <= 0)
                {
                    point.ResultConditions.UpdateValue(building.Type, 0);
                    return;
                }

                sum += ((conditionValue * 1.0) / maxConditions[buildingCondition.EnvironmentBuilding.ToString()]) * buildingCondition.Priority;
            }

            var prioritySum = building.BuildingConditions.Select(a => a.Priority).Sum()
                            + building.TerrainConditions.Select(a => a.Priority).Sum();
            sum /= prioritySum;
            sum *= 1000;

            point.ResultConditions.UpdateValue(building.Type, (int)sum);
        }

        public int GetConditionValue(EnvironmentConditionBase environmentCondition, Coord baseCoord, Coord adjacentCoord)
        {
            int value;

            if (environmentCondition.Type == EnvironmentConditionType.LinearDecrease)
            {
                value = GetValueLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius);
            }
            else
            {
                value = GetValueOneLevel();
            }

            if (environmentCondition.IsPositive == false)
            {
                value *= -1;
            }

            return value;
        }

        public Dictionary<string, int> GetMaxConditions(Building building)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var buildingCondition in building.BuildingConditions)
            {
                var value = Math.Abs(_palette.GetMaxBuildingConditionValue(building.Type, buildingCondition.EnvironmentBuilding));

                maxConditions.Add(buildingCondition.EnvironmentBuilding.ToString(), value);
            }

            foreach (var terrainCondition in building.TerrainConditions)
            {
                var value = Math.Abs(_palette.GetMaxTerrainConditionValue(building.Type, terrainCondition.EnvironmentTerrain));

                maxConditions.Add(terrainCondition.EnvironmentTerrain.ToString(), value);
            }

            return maxConditions;
        }

        public void UpdateBuildingEffects(Coord baseCoord)
        {
            foreach (var buildingLayerItem in _palette[baseCoord].Buildings.GetAll())
            {
                foreach (var building in Configs.Buildings.Values)
                {
                    var pointBuilding = Configs.Buildings[buildingLayerItem.BuildingType];

                    var effectedBuildoings = building.BuildingConditions.Where(a => a.EnvironmentBuilding == pointBuilding.Type);

                    foreach (var effectedBuildoing in effectedBuildoings)
                    {
                        var coords = baseCoord.GetAdjacentCoordsBeyond(effectedBuildoing.Radius, _palette.Width, _palette.Height);

                        foreach (var adjacentCoord in coords)
                        {
                            var value = GetConditionValue(effectedBuildoing, baseCoord, adjacentCoord);

                            _palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, pointBuilding.Type, value, buildingLayerItem.TownName);
                        }
                    }
                }
            }
        }

        public int GetValueLinearDecrease(Coord baseCoord, Coord adjacentCoord, int radius)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, _palette.Width, _palette.Height)) * (100.0 / radius));
        }

        public int GetValueOneLevel()
        {
            return 100;
        }
    }
}
