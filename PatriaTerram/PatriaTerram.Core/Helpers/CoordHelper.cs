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
        public static IEnumerable<Coord> GetAdjacentCoords(this Coord center, int radius)
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

        public static IEnumerable<Coord> GetPositiveAdjacentCoords(this Coord center, int radius, int width = int.MaxValue, int height = int.MaxValue)
        {
            return GetAdjacentCoords(center, radius)
                .Where(a => a.IsPositive)
                .Where(a => a.X < width && a.Y < height);
        }

        public static IEnumerable<Coord> GetAdjacentCoordsBeyond(this Coord center, int radius, int width, int height)
        {
            var coords = GetAdjacentCoords(center, radius);

            foreach (var coord in coords)
            {
                var x = coord.X;
                var y = coord.Y;

                if(x < 0)
                {
                    x += width;
                } else if(x >= width)
                {
                    x -= width;
                }

                if (y < 0)
                {
                    y += height;
                }
                else if (y >= height)
                {
                    y -= height;
                }

                coord.X = x;
                coord.Y = y;
            }

            return coords;
        }

        public static double Distance(this Coord c1, Coord c2)
        {
            return Math.Sqrt(Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2));
        }

        public static double DistanceBeyond(this Coord c1, Coord c2, int width, int height)
        {
            var dx1 = Math.Abs(c2.X - c1.X);
            var dx2 = width - dx1;

            var dy1 = Math.Abs(c2.Y - c1.Y);
            var dy2 = height - dy1;

            var minX = new[] { dx1, dx2 }.Min();
            var minY = new[] { dy1, dy2 }.Min();

            return Math.Sqrt(Math.Pow(minX, 2) + Math.Pow(minY, 2));
        }        
    }
}
