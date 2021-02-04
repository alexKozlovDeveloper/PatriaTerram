using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class Terrain
    {
        public string Name { get; set; }

        public bool IsAffectColor { get; set; }

        public string[] IntolerableTerrains { get; set; }

        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }


        public static Dictionary<string, Terrain> GetTerrains()
        {
            return new Dictionary<string, Terrain>
            {
                {
                    Constants.Altitude,
                    new Terrain
                    {
                        Name = Constants.Altitude,
                        IsAffectColor = false,
                        IntolerableTerrains = new string[] {},
                        ColorR = 0,
                        ColorG = 0,
                        ColorB = 0
                    }
                },
                {
                    Constants.FertileSoil,
                    new Terrain
                    {
                        Name = Constants.FertileSoil,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[] 
                        {
                            Constants.Ocean,
                            Constants.Stone,
                            Constants.Mountains,
                            Constants.Beach,
                            Constants.Lake
                        },
                        ColorR = 0,
                        ColorG = 120,
                        ColorB = 0
                    }
                },
                {
                    Constants.Ground,
                    new Terrain
                    {
                        Name = Constants.Ground,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[]
                        {
                            Constants.Ocean,
                            Constants.Mountains,
                            Constants.Beach,
                            Constants.Lake,
                            //Constants.Stone
                        },
                        ColorR = 0,
                        ColorG = 220,
                        ColorB = 0
                    }
                },
                {
                    Constants.Lake,
                    new Terrain
                    {
                        Name = Constants.Lake,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[] 
                        {
                            Constants.Ocean,
                            Constants.Mountains,
                            Constants.Stone,
                            Constants.Beach
                        },
                        ColorR = 0,
                        ColorG = 0,
                        ColorB = 255
                    }
                },
                {
                    Constants.Ocean,
                    new Terrain
                    {
                        Name = Constants.Ocean,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[] {},
                        ColorR = 0,
                        ColorG = 0,
                        ColorB = 128
                    }
                },
                {
                    Constants.Stone,
                    new Terrain
                    {
                        Name = Constants.Stone,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[] 
                        {
                            Constants.Ocean,
                            Constants.Mountains,
                            Constants.Lake
                        },
                        ColorR = 0,
                        ColorG = 0,
                        ColorB = 0
                    }
                },
                {
                    Constants.Wood,
                    new Terrain
                    {
                        Name = Constants.Wood,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[] 
                        {
                            Constants.Ocean,
                            Constants.Mountains,
                            Constants.Beach,
                            Constants.Lake,
                            Constants.Stone
                        },
                        ColorR = 128,
                        ColorG = 128,
                        ColorB = 0
                    }
                },
                {
                    Constants.Mountains,
                    new Terrain
                    {
                        Name = Constants.Mountains,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[]
                        {
                            Constants.Ground,
                            Constants.FertileSoil,
                            Constants.Lake,
                            Constants.Stone,
                            Constants.Wood
                        },
                        ColorR = 100,
                        ColorG = 100,
                        ColorB = 100
                    }
                },
                {
                    Constants.Beach,
                    new Terrain
                    {
                        Name = Constants.Beach,
                        IsAffectColor = true,
                        IntolerableTerrains = new string[]
                        {
                            
                        },
                        ColorR = 255,
                        ColorG = 255,
                        ColorB = 0
                    }
                }
            };
        }
    }
}
