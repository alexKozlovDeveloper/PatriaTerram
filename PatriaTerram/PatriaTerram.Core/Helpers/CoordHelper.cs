using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class CoordHelper
    {
        public static IEnumerable<Coord> GetAdjacentCoords(Coord center, int radius)
        {
            var result = new List<Coord>();

            int x = 0;
            int y = radius;

            while(x <= radius)
            {
                y = (int)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(x, 2));

                for (int i = 0; i <= y; i++)
                {
                    result.Add(new Coord 
                    { 
                        X = x + center.X,
                        Y = i + center.Y
                    });
                    result.Add(new Coord
                    {
                        X = x + center.X,
                        Y = -i + center.Y
                    });
                    result.Add(new Coord
                    {
                        X = -x + center.X,
                        Y = i + center.Y
                    });
                    result.Add(new Coord
                    {
                        X = -x + center.X,
                        Y = -i + center.Y
                    });
                }

                x++;
            }

            return result.Distinct();
        }

        public static IEnumerable<Coord> GetPositiveAdjacentCoords(Coord center, int radius, int width = int.MaxValue, int height = int.MaxValue)
        {
            return GetAdjacentCoords(center, radius)
                .Where(a => a.IsPositive)
                .Where(a => a.X < width && a.Y < height);
        }

        public static double Distance(this Coord c1, Coord c2)
        {
            return Math.Sqrt(Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2));
        }
    }
}
