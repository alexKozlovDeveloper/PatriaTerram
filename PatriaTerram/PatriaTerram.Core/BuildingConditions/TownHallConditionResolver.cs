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
        private Dictionary<string, int> _conditions = new Dictionary<string, int>
        {
            {
                Constants.Stone,
                20
            },
            {
                Constants.Wood,
                10
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

        public void Resolve(Palette palette, Coord pointCoord)
        {
            var point = palette.Points[pointCoord.X][pointCoord.Y];

            for (int i = 0; i < point.Components.Count; i++)
            {
                var terrainName = point.Components[i].Terrain.Name;

                if (_conditions.Keys.Contains(terrainName))
                {
                    var radius = _conditions[terrainName];

                    //var coords = CoordHelper.GetPositiveAdjacentCoords(new Coord { X = pointCoord.X, Y = pointCoord.Y }, radius, palette.Width, palette.Height);
                    var coords = CoordHelper.GetAdjacentCoordsBeyond(new Coord { X = pointCoord.X, Y = pointCoord.Y }, radius, palette.Width, palette.Height);

                    foreach (var coord in coords)
                    {
                        int value = (int)((radius - (int)coord.DistanceBeyond(pointCoord, palette.Width, palette.Height )) * (100.0 / radius));

                        if(value < 0)
                        {

                        }

                        palette.Points[coord.X][coord.Y].AddBuildingConditions(new Models.BuildingConditions
                        {
                            BuildingType = Constants.TownHall,
                            Value = value
                        });
                    }
                }
            }
        }
    }
}
