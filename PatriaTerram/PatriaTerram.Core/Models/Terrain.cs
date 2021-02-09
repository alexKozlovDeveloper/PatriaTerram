using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class Terrain
    {
        public string Name { get; set; }
        public bool IsAffectColor { get; set; }
        public string[] IntolerableTerrains { get; set; }

        //public int ColorR { get; set; }
        //public int ColorG { get; set; }
        //public int ColorB { get; set; }

        public Color Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
