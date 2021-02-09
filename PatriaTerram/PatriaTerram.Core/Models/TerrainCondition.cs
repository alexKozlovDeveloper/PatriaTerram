using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class TerrainCondition
    {
        public string Terrain { get; set; }
        public int Radius { get; set; }
        public int Priority { get; set; }
    }
}
