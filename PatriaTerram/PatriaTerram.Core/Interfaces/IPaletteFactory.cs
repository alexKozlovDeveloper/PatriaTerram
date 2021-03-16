using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Interfaces
{
    public interface IPaletteFactory<Point> where Point : TerrainPalettePoint
    {
        Palette<Point> GetPalette();
    }
}
