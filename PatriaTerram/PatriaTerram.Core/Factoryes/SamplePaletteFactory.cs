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
            var model = new Palette
            {
                Points = new PalettePoint[2][]
            };

            model.Points[0] = new PalettePoint[2];
            model.Points[1] = new PalettePoint[2];

            model[0, 0] = new PalettePoint();

            model[0, 0] = new PalettePoint {  };
            model[0, 1] = new PalettePoint {  };
            model[1, 0] = new PalettePoint {  };
            model[1, 1] = new PalettePoint {  };

            return model;
        }
    }
}
