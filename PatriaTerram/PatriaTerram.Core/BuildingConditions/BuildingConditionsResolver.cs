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
        private Palette _palette;

        public BuildingConditionsResolver(Palette palette)
        {
            _palette = palette;
        }

        public void ResolvePoint(Coord baseCoord, Building building)
        {
            var basePoint = _palette[baseCoord];

            foreach (var terrainType in basePoint.Terrains.TerrainTypes)
            {
                var environmentCondition = building.EnvironmentConditions.FirstOrDefault(a => a.Environment == terrainType.ToString());

                if (environmentCondition == null) { continue; }

                var radius = environmentCondition.Radius;
                var adjacentCoords = baseCoord.GetAdjacentCoordsBeyond(radius, _palette.Width, _palette.Height);

                foreach (var adjacentCoord in adjacentCoords)
                {
                    var value = GetConditionValue(environmentCondition, baseCoord, adjacentCoord);

                    _palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, terrainType.ToString(), value);
                }
            }
        }

        public int GetConditionValue(EnvironmentCondition environmentCondition, Coord baseCoord, Coord adjacentCoord)
        {
            var value = 0;

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

        public void FinalResolve(Building building)
        {
            var maxConditions = GetMaxConditions(building);

            foreach (var point in _palette.AllPoints)
            {
                if (point.BuildingConditions.IsHasBuildingCondition(building.Type) == false) { continue; }

                double sum = 0;

                foreach (var terrainCondition in building.EnvironmentConditions)
                {
                    if (point.BuildingConditions.IsHasEnvironment(building.Type, terrainCondition.Environment) == false) { continue; }

                    var conditionValue = point.BuildingConditions.GetValue(building.Type, terrainCondition.Environment);

                    sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.Environment]) * terrainCondition.Priority;
                }

                sum /= building.EnvironmentConditions.Select(a => a.Priority).Sum();
                sum *= 1000;

                point.BuildingConditions.UpdateValue(building.Type, Constants.Result, (int)sum);
            }
        }

        private Dictionary<string, int> GetMaxConditions(Building building)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var terrainCondition in building.EnvironmentConditions)
            {
                var value = Math.Abs(_palette.GetMaxBuildingConditionValue(building.Type, terrainCondition.Environment));

                maxConditions.Add(terrainCondition.Environment, value);
            }

            return maxConditions;
        }

        public void UpdateBuildingEffects(Coord baseCoord)
        {
            foreach (var pointBuildingType in _palette[baseCoord].Buildings.GetBuildings())
            {
                foreach (var building in Configs.Buildings.Values)
                {
                    var pointBuilding = Configs.Buildings[pointBuildingType];

                    var effectedBuildoings = building.EnvironmentConditions.Where(a => a.Environment == pointBuilding.Type.ToString());

                    foreach (var effectedBuildoing in effectedBuildoings)
                    {
                        var coords = baseCoord.GetAdjacentCoordsBeyond(effectedBuildoing.Radius, _palette.Width, _palette.Height);

                        foreach (var adjacentCoord in coords)
                        {
                            var value = GetConditionValue(effectedBuildoing, baseCoord, adjacentCoord);

                            _palette[adjacentCoord].BuildingConditions.AddConditionValue(building.Type, pointBuilding.Type.ToString(), value);
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
