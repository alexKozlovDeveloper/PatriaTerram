using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Interfaces
{
    public interface IPaletteFactory<Point>
    {
        Palette<Point> GetPalette();
    }
}
