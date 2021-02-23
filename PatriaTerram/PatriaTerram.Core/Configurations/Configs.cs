using Newtonsoft.Json;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using System.Collections.Generic;
using System.IO;

namespace PatriaTerram.Core.Configurations
{
    public static class Configs
    {
        public static string TerrainsJsonFilePath = @"Configurations\Files\Terrains.json";
        public static string PaletteConfigurationJsonFilePath = @"Configurations\Files\PaletteConfigurations.json";

        private static Dictionary<TerrainType, Terrain> _terrains;
        private static Dictionary<string, PaletteConfiguration> _paletteConfigs;

        

        public static Dictionary<TerrainType, Terrain> Terrains
        {
            get
            {
                if (_terrains == null)
                {
                    var json = File.ReadAllText(TerrainsJsonFilePath);
                    _terrains = JsonConvert.DeserializeObject<Dictionary<TerrainType, Terrain>>(json);
                }

                return _terrains;
            }
        }

        public static Dictionary<string, PaletteConfiguration> PaletteConfigs
        {
            get
            {
                if (_paletteConfigs == null)
                {
                    var json = File.ReadAllText(PaletteConfigurationJsonFilePath);
                    _paletteConfigs = JsonConvert.DeserializeObject<Dictionary<string, PaletteConfiguration>>(json);
                }

                return _paletteConfigs;
            }
        }
    }
}
