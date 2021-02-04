using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class Palette
    {
        public PalettePoint[][] Points;

        public int Width
        {
            get
            {
                if (Points == null) { return 0; }

                return Points.Length;
            }
        }
        public int Height
        {
            get
            {
                if (Points == null) { return 0; }
                if (Points.Length == 0) { return 0; }
                if (Points[0] == null) { return 0; }

                return Points[0].Length;
            }
        }
        public int Depth
        {
            get
            {
                var max = Points.Select(a => a.Max(b => b.Components.Count))
                                .Max();

                return max;
            }
        }

        public PalettePoint this[int i, int j]
        {
            get { return Points[i][j]; }
            set { Points[i][j] = value; }
        }
    }
}