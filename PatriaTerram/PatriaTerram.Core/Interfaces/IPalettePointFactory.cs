using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Interfaces
{
    public interface IPalettePointFactory<Point>
    {
        Point Create(int x, int y);
    }
}
