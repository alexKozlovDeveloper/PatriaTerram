using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Conditions.Resolvers
{
    public class PointResultConditionResolver : BasePointResolver
    {
        public PointResultConditionResolver(Palette<ConditionPalettePoint> palette) : base(palette)
        {

        }

        public void Resolve(ConditionPalettePoint point, string townName, Building building, Dictionary<string, Range> maxConditions)
        {
            double sum = 0;

            foreach (var terrainCondition in building.TerrainConditions)
            {
                var conditionValue = point.TerrainConditions.GetValue(building.Type, terrainCondition.EnvironmentTerrain);

                var resultValue = GetResultValue(conditionValue, maxConditions[terrainCondition.EnvironmentTerrain.ToString()], terrainCondition.Priority);

                if (terrainCondition.IsRequired == true && terrainCondition.IsPositive == true && resultValue <= 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                if (terrainCondition.IsRequired == true && terrainCondition.IsPositive == false && resultValue > 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                if (terrainCondition.IsPositive == false)
                {
                    resultValue *= -1;
                }

                sum += resultValue;
            }

            foreach (var buildingCondition in building.BuildingConditions)
            {
                var conditionValue = point.BuildingConditions.GetValue(townName, building.Type, buildingCondition.EnvironmentBuilding);

                if (buildingCondition.TownCondition == TownCondition.AnyTown)
                {
                    conditionValue = point.BuildingConditions.GetValue(building.Type, buildingCondition.EnvironmentBuilding);
                }

                var resultValue = GetResultValue(conditionValue, maxConditions[buildingCondition.EnvironmentBuilding.ToString()], buildingCondition.Priority);

                if (buildingCondition.IsRequired == true && buildingCondition.IsPositive == true && resultValue <= 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                if (buildingCondition.IsRequired == true && buildingCondition.IsPositive == false && resultValue > 0)
                {
                    point.ResultConditions.UpdateValue(townName, building.Type, 0);
                    return;
                }

                if(buildingCondition.IsPositive == false)
                {
                    resultValue *= -1;
                }

                sum += resultValue;
            }

            var prioritySum = building.BuildingConditions.Select(a => a.Priority).Sum()
                            + building.TerrainConditions.Select(a => a.Priority).Sum();

            sum /= prioritySum;
            sum *= 100;

            point.ResultConditions.UpdateValue(townName, building.Type, (int)sum);
        }
    }
}
