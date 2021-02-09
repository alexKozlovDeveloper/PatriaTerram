using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.BuildingConditions
{
    public class TownHallConditionResolver : IConditionsResolver
    {
        public string ConditionName = Constants.TownHall;

        private List<TerrainCondition> _terrainConditions = new List<TerrainCondition>
        {
            new TerrainCondition
            {
                Terrain = Constants.Stone,
                Radius = 15,
                Priority = 15
            },
            new TerrainCondition
            {
                Terrain = Constants.Wood,
                Radius = 7,
                Priority = 10
            },
            new TerrainCondition
            {
                Terrain = Constants.FertileSoil,
                Radius = 5,
                Priority = 15
            },
            new TerrainCondition
            {
                Terrain = Constants.Lake,
                Radius = 8,
                Priority = 20
            }
        };

        public void ResolvePoint(Palette palette, Coord baseCoord)
        {
            var basePoint = palette[baseCoord];

            foreach (var terrain in basePoint.Terrains.Keys)
            {
                if (_terrainConditions.FirstOrDefault(a => a.Terrain == terrain) != null)
                {
                    var radius = _terrainConditions.FirstOrDefault(a => a.Terrain == terrain).Radius;

                    var coords = baseCoord.GetAdjacentCoordsBeyond(radius, palette.Width, palette.Height);

                    foreach (var adjacentCoord in coords)
                    {
                        int value = GetValue(baseCoord, adjacentCoord, radius, palette.Width, palette.Height);

                        palette[adjacentCoord].AddBuildingConditions(ConditionName, terrain, value);
                    }
                }
            }
        }

        public void FinalResolve(Palette palette)
        {
            var maxConditions = new Dictionary<string, int>();

            foreach (var terrainCondition in _terrainConditions)
            {
                maxConditions.Add(terrainCondition.Terrain, palette.GetMaxBuildingConditionValue(ConditionName, terrainCondition.Terrain));
            }

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var conditions = palette[x, y].BuildingConditions[Constants.TownHall];

                    double sum = 0;

                    foreach (var terrainCondition in _terrainConditions)
                    {
                        if(conditions.TerrainConditionValues.Keys.Contains(terrainCondition.Terrain) == false) { continue; }

                        var conditionValue = conditions.TerrainConditionValues[terrainCondition.Terrain];

                        sum += ((conditionValue * 1.0) / maxConditions[terrainCondition.Terrain]) * terrainCondition.Priority;
                    }

                    sum /= _terrainConditions.Select(a => a.Priority).Sum();
                    sum *= 1000;


                    conditions.AddConditionValue("result", (int)sum);
                }
            }
        }

        private int GetValue(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, width, height)) * (100.0 / radius));
        }
    }
}
