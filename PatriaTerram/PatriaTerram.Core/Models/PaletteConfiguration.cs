using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class PaletteConfiguration
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Seed { get; set; }
        public int MaxAltitudeValue { get; set; }
        public int OceanEdge { get; set; }
        public int MountainsEdge { get; set; }
        public Range FertileSoilRange { get; set; }
        public Range WoodRange { get; set; }
        public Range StoneRange { get; set; }
        public Range LakeRange { get; set; }
        public int BeachSize { get; set; }
        public int SmoothingSize { get; set; }
    }
}
