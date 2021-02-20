using Newtonsoft.Json;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Factoryes;
using System.Collections.Generic;
using System.IO;

namespace PatriaTerram.Console
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static void WriteTerrainsConfigToJsonFile() 
        {
            var terrains = new Dictionary<TerrainType, Terrain>
            {
                {
                    TerrainType.Altitude,
                    new Terrain
                    {
                        Type = TerrainType.Altitude,
                        IntolerableTerrains = new TerrainType[]
                        {
                            TerrainType.Beach,
                            TerrainType.FertileSoil
                        },
                        IsAffectColor = true,
                        Color = new Color
                        {
                            R = 1,
                            G = 2,
                            B = 3
                        }
                    }
                },
                {
                    TerrainType.Beach,
                    new Terrain
                    {
                        Type = TerrainType.Beach,
                        IntolerableTerrains = new TerrainType[]
                        {
                            TerrainType.Altitude,
                            TerrainType.Stone
                        },
                        IsAffectColor = true,
                        Color = new Color
                        {
                            R = 4,
                            G = 5,
                            B = 6
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(terrains);

            File.WriteAllText("terrains.json", json);
        }
    }
}
