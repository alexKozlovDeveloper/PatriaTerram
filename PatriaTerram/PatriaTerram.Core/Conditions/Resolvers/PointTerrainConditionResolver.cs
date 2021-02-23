using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Conditions.Resolvers
{
    public class PointTerrainConditionResolver : BasePointResolver
    {
        public PointTerrainConditionResolver(Palette palette) : base(palette)
        {

        }

        public void Resolve(Coord baseCoord, Building building)
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

                    _palette[adjacentCoord].TerrainConditions.AddConditionValue(building.Type, terrainType, value, environmentCondition);
                }
            }
        }
    }
}
