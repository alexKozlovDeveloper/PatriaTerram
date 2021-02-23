using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Models;

namespace PatriaTerram.Core
{
    public static class Example
    {
        public static Palette<TerrainPalettePoint> GetTerrainPalette()
        {
            var factory = new TerrainPaletteFactory<TerrainPalettePoint>(Configs.PaletteConfigs["main"]);

            var model = factory.GetPalette();

            return model;
        }
    }
}
