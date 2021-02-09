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

        private Dictionary<string, int> _terrainConditions = new Dictionary<string, int>
        {
            {
                Constants.Stone,
                15
            },
            {
                Constants.Wood,
                7
            },
            {
                Constants.FertileSoil,
                5
            },
            {
                Constants.Lake,
                8
            }
        };

        public void Resolve(Palette palette, Coord baseCoord)
        {
            var basePoint = palette[baseCoord];

            foreach (var terrain in basePoint.Terrains.Keys)
            {
                if (_terrainConditions.Keys.Contains(terrain.Name))
                {
                    var radius = _terrainConditions[terrain.Name];

                    var coords = baseCoord.GetAdjacentCoordsBeyond(radius, palette.Width, palette.Height);
                    
                    foreach (var adjacentCoord in coords)
                    {
                        int value = GetValue(baseCoord, adjacentCoord, radius, palette.Width, palette.Height);

                        palette[adjacentCoord].AddBuildingConditions(ConditionName, terrain.Name, value);
                    }
                }
            }

            //for (int i = 0; i < point.Terrains.Count; i++)
            //{
            //    var terrainName = point.Terrains[i].Terrain.Name;

            //    if (_terrainConditions.Keys.Contains(terrainName))
            //    {
            //        var radius = _terrainConditions[terrainName];

            //        var coords = CoordHelper.GetAdjacentCoordsBeyond(new Coord { X = pointCoord.X, Y = pointCoord.Y }, radius, palette.Width, palette.Height);

            //        foreach (var coord in coords)
            //        {
            //            int value = (int)((radius - (int)coord.DistanceBeyond(pointCoord, palette.Width, palette.Height)) * (100.0 / radius));

            //            point.AddBuildingConditions(ConditionName, terrainName, value);

            //            //palette.Points[coord.X][coord.Y].AddBuildingConditions(new Models.BuildingConditions
            //            //{
            //            //    BuildingType = Constants.TownHall,
            //            //    Value = value
            //            //});
            //        }
            //    }
            //}
        }

        private int GetValue(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, width, height)) * (100.0 / radius));
        }
    }
}
