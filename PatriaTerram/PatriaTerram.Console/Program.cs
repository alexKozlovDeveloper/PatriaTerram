using Newtonsoft.Json;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Logging;
using PatriaTerram.Core.Models;
using PatriaTerram.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PatriaTerram.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var tailor = new StoryTailor(new ConsoleLogger());

            var model = tailor.Tell();

            var path = $"result{DateTime.Now.ToString("yyyy_MM_dd HH-mm-ss")}.png";

            Console.WriteLine($"Writing Image...");            
            WriteImage(model, path);
        }

        private static void WriteImage(Palette<ConditionPalettePoint> model, string path)
        {
            var iamge = GetImage(model);
            iamge.Save(path);
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
                        Color = new Core.Configurations.Entityes.Color
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
                        Color = new Core.Configurations.Entityes.Color
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

        private static Bitmap GetImage(Palette<ConditionPalettePoint> model)
        {
            var width = model.Width;
            var height = model.Height;
            var multiplayer = 5;

            var image = new Bitmap(width * multiplayer, height * multiplayer);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    System.Drawing.Color color;
                    var point = model[x, y];

                    if (point.Buildings.IsHasAnyBuildings() == false)
                    {
                        var itemValue = 1;//point.Terrains.GetTerrainValue(TerrainType.Result);
                        var resultColor = point.GetPointColor();

                        color = System.Drawing.Color.FromArgb((resultColor.R * itemValue),
                                                                  (resultColor.G * itemValue),
                                                                  (resultColor.B * itemValue));
                    }
                    else
                    {
                        if(point.Buildings.Count() > 1)
                        {
                            var building = point.Buildings.GetAll().FirstOrDefault(a => a.BuildingType != BuildingType.Road);

                            var buildingColor = ConditionConfigs.Buildings[building.BuildingType].Color.GetReduceredColor(1.25);

                            color = System.Drawing.Color.FromArgb(buildingColor.R, buildingColor.G, buildingColor.B);                            
                        }
                        else
                        {
                            var building = point.Buildings.GetAll().FirstOrDefault();

                            color = System.Drawing.Color.FromArgb(
                                          (ConditionConfigs.Buildings[building.BuildingType].Color.R),
                                          (ConditionConfigs.Buildings[building.BuildingType].Color.G),
                                          (ConditionConfigs.Buildings[building.BuildingType].Color.B));
                        }
                    }               

                    for (int i = 0; i < multiplayer; i++)
                    {
                        for (int j = 0; j < multiplayer; j++)
                        {
                            image.SetPixel(x * multiplayer + i, y * multiplayer + j, color);
                        }
                    }
                }
            }

            return image;
        }
    }
}
