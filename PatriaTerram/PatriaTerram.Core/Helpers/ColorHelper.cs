using PatriaTerram.Core.Configurations.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class ColorHelper
    {
        public static Color GetHalfColor(this Color color)
        {
            return color.GetReduceredColor(2);
        }

        public static Color GetReduceredColor(this Color color, double reducer)
        {
            return new Color
            {
                R = (int)(color.R / reducer),
                G = (int)(color.G / reducer),
                B = (int)(color.B / reducer)
            };
        }
    }
}
