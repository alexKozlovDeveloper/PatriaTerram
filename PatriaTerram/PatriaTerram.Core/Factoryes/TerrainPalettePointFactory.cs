using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Factoryes
{
    public class TerrainPalettePointFactory : IPalettePointFactory<TerrainPalettePoint>
    {
        public TerrainPalettePoint Create(int x, int y)
        {
            return new TerrainPalettePoint(x, y);
        }
    }
}
