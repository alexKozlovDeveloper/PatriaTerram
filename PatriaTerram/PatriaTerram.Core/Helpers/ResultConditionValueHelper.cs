using AStarAlgorithm.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class ResultConditionValueHelper
    {
        public static int GetValueLinearDecrease(Coord baseCoord, Coord adjacentCoord, int radius, int width, int height)
        {
            return (int)((radius - (int)baseCoord.DistanceBeyond(adjacentCoord, width, height)) * (100.0 / radius));
        }

        public static int GetValueOneLevel()
        {
            return 100;
        }

        public static int GetValueRingLinearDecrease(Coord baseCoord, Coord adjacentCoord, int radius, int innerRadius, int width, int height)
        {
            int distance = (int)baseCoord.DistanceBeyond(adjacentCoord, width, height);

            if(distance < innerRadius)
            {
                return 0;
            }

            return (int)((radius - distance) * (100.0 / radius));
        }

        public static int GetValueRingOneLevel(Coord baseCoord, Coord adjacentCoord, int radius, int innerRadius, int width, int height)
        {
            int distance = (int)baseCoord.DistanceBeyond(adjacentCoord, width, height);

            if(distance < innerRadius)
            {
                return 0;
            }

            return 100;
        }
    }
}
