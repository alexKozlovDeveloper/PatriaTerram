using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class Terrain
    {
        public TerrainType Type { get; set; }
        public bool IsAffectColor { get; set; }
        public TerrainType[] IntolerableTerrains { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
