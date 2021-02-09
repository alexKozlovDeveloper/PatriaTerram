using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Factoryes
{
    public class SamplePaletteFactory : IPaletteFactory
    {
        public Palette GetPalette()
        {
            var points = new PalettePoint[2][];

            points[0] = new PalettePoint[2];
            points[1] = new PalettePoint[2];

            points[0][0] = new PalettePoint {  };
            points[0][1] = new PalettePoint {  };
            points[1][0] = new PalettePoint {  };
            points[1][1] = new PalettePoint {  };

            return new Palette(points);
        }
    }
}
