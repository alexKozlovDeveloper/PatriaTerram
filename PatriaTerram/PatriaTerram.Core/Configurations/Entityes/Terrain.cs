using PatriaTerram.Core.Enums;

namespace PatriaTerram.Core.Configurations.Entityes
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
