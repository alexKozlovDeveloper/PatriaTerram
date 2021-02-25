using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Condition.Helpers
{
    public static class TownHelper
    {
        //public static List<Coord> GetAllTownHallCoords(this Palette<ConditionPalettePoint> palette)
        //{
        //    var townNames = palette.GetAllTownNames();

        //    var result = new List<Coord>();

        //    foreach (var townName in townNames)
        //    {
        //        result.AddRange(palette.GetAllBuildingCoords(BuildingType.TownHall, townName));
        //    }

        //    return result;
        //}

        public static List<Coord> GetAllBuildingCoords(this Palette<ConditionPalettePoint> palette, BuildingType buildingType)
        {
            var result = new List<Coord>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    if(point.Buildings.GetBuildingTypes().Contains(buildingType) == true)
                    {
                        result.Add(new Coord(x, y));
                    }
                }
            }

            return result;
        }

        public static List<Coord> GetAllBuildingCoords(this Palette<ConditionPalettePoint> palette, BuildingType buildingType, string townName)
        {
            var result = new List<Coord>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    if (point.Buildings.IsHasBuildings(buildingType, townName))
                    {
                        result.Add(new Coord(x, y));
                    }
                }
            }

            return result;
        }

        public static List<Coord> GetAllBuildingCoords(this Palette<ConditionPalettePoint> palette)
        {
            var result = new List<Coord>();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    var point = palette[x, y];

                    if (point.Buildings.IsHasAnyBuildings() == true)
                    {
                        result.Add(new Coord(x, y));
                    }
                }
            }

            return result;
        }
    }
}
