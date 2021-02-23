using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Conditions.Resolvers
{
    public class PointBuildingConditionResolver : BasePointResolver
    {
        public PointBuildingConditionResolver(Palette<ConditionPalettePoint> palette) : base(palette)
        {

        }

        public void Resolve(Coord baseCoord, string townName, Building building)
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

                    _palette[adjacentCoord].BuildingConditions.AddConditionValue(townName, building.Type, environmentCondition.EnvironmentBuilding, value, environmentCondition);
                }
            }
        }

        public void UpdateBuildingEffects(Coord baseCoord, Dictionary<BuildingType, Building> configBuilding)
        {
            foreach (var buildingLayerItem in _palette[baseCoord].Buildings.GetAll())
            {
                var pointBuilding = configBuilding[buildingLayerItem.BuildingType];

                foreach (var building in configBuilding.Values)
                {
                    var effectedBuildoings = building.BuildingConditions.Where(a => a.EnvironmentBuilding == pointBuilding.Type);

                    foreach (var effectedBuildoing in effectedBuildoings)
                    {
                        var coords = baseCoord.GetAdjacentCoordsBeyond(effectedBuildoing.Radius, _palette.Width, _palette.Height);

                        foreach (var adjacentCoord in coords)
                        {
                            var value = GetConditionValue(effectedBuildoing, baseCoord, adjacentCoord);

                            _palette[adjacentCoord].BuildingConditions.AddConditionValue(buildingLayerItem.TownName, building.Type, pointBuilding.Type, value, effectedBuildoing);
                        }
                    }
                }
            }
        }
    }
}
