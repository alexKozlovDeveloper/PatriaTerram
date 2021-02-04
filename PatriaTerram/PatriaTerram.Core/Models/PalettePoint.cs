using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        public List<Component> Components { get; }

        public PalettePoint()
        {
            Components = new List<Component>();
        }

        public void GetPointColor(out int r, out int g, out int b)
        {
            r = 0;
            g = 0;
            b = 0;

            int count = 0;

            foreach (var component in Components)
            {
                if(component.Terrain.IsAffectColor == false) { continue; }

                r += component.Terrain.ColorR;
                g += component.Terrain.ColorG;
                b += component.Terrain.ColorB;

                count++;
            }

            r /= count;
            g /= count;
            b /= count;
        }
    }
}