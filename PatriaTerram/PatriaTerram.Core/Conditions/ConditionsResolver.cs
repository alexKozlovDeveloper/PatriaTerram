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

        public void ResolveBuildingCondition(Coord baseCoord, string townName, Building building)
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

                    _palette[adjacentCoord].BuildingConditions.AddConditionValue(townName, building.Type, environmentCondition.EnvironmentBuilding, value);
                }
            }
        }

        public void ResolveResultCondition(PalettePoint point, string townName, Building building, Dictionary<string, Range> maxConditions)
        {
            double sum = 0;

            foreach (var terrainCondition in building.TerrainConditions)
            {
                var conditionValue = point.TerrainConditions.GetValue(building.Type, terrainCondition.EnvironmentTerrain);

                var resultValue = GetResultValue(conditionValue, maxConditions[terrainCondition.EnvironmentTerrain.ToString()], terrainCondition.Priority);

                if (terrainCondition.IsRequired == true && resultValue <= 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                sum += resultValue;
            }

            foreach (var buildingCondition in building.BuildingConditions)
            {
                var conditionValue = point.BuildingConditions.GetValue(townName, building.Type, buildingCondition.EnvironmentBuilding);

                if(buildingCondition.TownCondition == TownCondition.AnyTown)
                {
                    conditionValue = point.BuildingConditions.GetValue(building.Type, buildingCondition.EnvironmentBuilding);
                }

                var resultValue = GetResultValue(conditionValue, maxConditions[buildingCondition.EnvironmentBuilding.ToString()], buildingCondition.Priority);

                if (buildingCondition.IsRequired == true && resultValue <= 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                sum += resultValue;
            }

            var prioritySum = building.BuildingConditions.Select(a => a.Priority).Sum()
                            + building.TerrainConditions.Select(a => a.Priority).Sum();

            sum /= prioritySum;
            sum *= 100;

            point.ResultConditions.UpdateValue(townName, building.Type, (int)sum);
        }

        private int GetResultValue(int conditionValue, Range range, int priority)
        {
            double value = range.ToRangeValuePercent(conditionValue);

            double res = value * priority * 100;

            return (int)res;
        }

        public int GetConditionValue(EnvironmentConditionBase environmentCondition, Coord baseCoord, Coord adjacentCoord)
        {
            int value = 0;

            switch (environmentCondition.Type)
            {
                case EnvironmentConditionType.LinearDecrease:
                    value = ConditionsResolverHelper.GetValueLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.OneLevel:
                    value = ConditionsResolverHelper.GetValueOneLevel();
                    break;
                case EnvironmentConditionType.RingOneLevel:
                    value = ConditionsResolverHelper.GetValueRingOneLevel(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.RingLinearDecrease:
                    value = ConditionsResolverHelper.GetValueRingLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                default:
                    break;
            }


            if (environmentCondition.IsPositive == false)
            {
                value *= -1;
            }

            return value;
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

                            _palette[adjacentCoord].BuildingConditions.AddConditionValue(buildingLayerItem.TownName, building.Type, pointBuilding.Type, value);
                        }
                    }
                }
            }
        }
    }
}
