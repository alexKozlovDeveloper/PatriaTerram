using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Factoryes
{
    public class SamplePaletteFactory<Point> : IPaletteFactory<Point> where Point : new()
    {
        public Palette<Point> GetPalette()
        {
            var points = new Point[2][];

            points[0] = new Point[2];
            points[1] = new Point[2];

            points[0][0] = new Point { };
            points[0][1] = new Point { };
            points[1][0] = new Point { };
            points[1][1] = new Point { };

            return new Palette<Point>(points);
        }
    }
}
