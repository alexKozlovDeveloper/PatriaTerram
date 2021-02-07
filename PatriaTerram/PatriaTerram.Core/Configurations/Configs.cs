using Newtonsoft.Json;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Configurations
{
    public static class Configs
    {
        public static string TerrainsJsonFilePath = @"Configurations\Terrains.json";
        public static string PaletteConfigurationJsonFilePath = @"Configurations\PaletteConfigurations.json";

        private static Dictionary<string, Terrain> _terrains;
        private static Dictionary<string, PaletteConfiguration> _paletteConfigs;

        public static Dictionary<string, Terrain> Terrains 
        { 
            get
            {
                if(_terrains == null)
                {
                    var json = File.ReadAllText(TerrainsJsonFilePath);
                    _terrains = JsonConvert.DeserializeObject<Dictionary<string, Terrain>>(json);
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
