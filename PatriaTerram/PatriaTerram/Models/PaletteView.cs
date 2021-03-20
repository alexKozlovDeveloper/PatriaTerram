using PatriaTerram.Core;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using PatriaTerram.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class PaletteView
    {
        public PalettePointView[][] Points { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public PaletteContext Context { get; set; }

        public PaletteView(Palette<ConditionPalettePoint> palette)
        {
            Context = new PaletteContext(palette);

            Width = palette.Width;
            Height = palette.Height;

            Points = new PalettePointView[Width][];

            for (int x = 0; x < Width; x++)
            {
                Points[x] = new PalettePointView[Height];

                for (int y = 0; y < Height; y++)
                {
                    Points[x][y] = new PalettePointView(palette[x, y], Context);
                }
            }
        }
    }
}