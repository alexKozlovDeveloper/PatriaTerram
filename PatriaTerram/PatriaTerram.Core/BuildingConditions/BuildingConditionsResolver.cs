using PatriaTerram.Core.Configurations;
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
                if (building.EnvironmentConditions.FirstOrDefault(a => a.Environment == terrain) != null)
                {
                    var radius = building.EnvironmentConditions.FirstOrDefault(a => a.Environment == terrain).Radius;

                    var coords = baseCoord.GetAdjacentCoordsBeyond(radius, palette.Width, palette.Height);

                    foreach (var adjacentCoord in coords)
                    {
                        int value = GetValue(baseCoord, adjacentCoord, radius, palette.Width, palette.Height);

                        palette[adjacentCoord].AddBuildingConditions($"{building.Name}", terrain, value);
                    }
                }
            }
        }

        public void FinalResolve(Palette palette, Building building)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var terrainCondition in building.EnvironmentConditions)
            {
                maxConditions.Add(terrainCondition.Environment, palette.GetMaxBuildingConditionValue(building.Name, terrainCondition.Environment));
            }

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var conditions = palette[x, y].BuildingConditions[Constants.TownHall];

                    double sum = 0;

                    foreach (var terrainCondition in building.EnvironmentConditions)
                    {
                        if (conditions.EnvironmentConditionValues.Keys.Contains(terrainCondition.Environment) == false) { continue; }

                        var conditionValue = conditions.EnvironmentConditionValues[terrainCondition.Environment];

                        sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.Environment]) * terrainCondition.Priority;
                    }

                    sum /= building.EnvironmentConditions.Select(a => a.Priority).Sum();
                    sum *= 1000;


                    conditions.AddConditionValue("result", (int)sum);
                }
            }
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
                            int value = GetValue(baseCoord, adjacentCoord, effectedBuildoing.Radius, palette.Width, palette.Height);

                            palette[adjacentCoord].AddBuildingConditions($"{building.Name}", pointBuilding.Name, value);
                        }
                    }
                }
            }
        }

        public static int GetValue(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, width, height)) * (100.0 / radius));
        }
    }
}
