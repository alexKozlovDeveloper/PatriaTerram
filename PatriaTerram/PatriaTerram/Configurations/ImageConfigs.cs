using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Configurations
{
    public static class ImageConfigs
    {
        private static Dictionary<TerrainType, string> _terrainTextureMaping;
        private static Dictionary<BuildingType, string> _buildingTextureMaping;

        public static Dictionary<TerrainType, string> TerrainTextureMaping
        {
            get
            {
                if (_terrainTextureMaping == null)
                {
                    var urlBase = "~/Content/Terrains/";

                    _terrainTextureMaping = new Dictionary<TerrainType, string>
                    {
                        {
                            TerrainType.Beach,
                            urlBase + "Beach.png"
                        },
                        {
                            TerrainType.FertileSoil,
                            urlBase + "FertileSoil.png"
                        },
                        {
                            TerrainType.Ground,
                            urlBase + "Ground.png"
                        },
                        {
                            TerrainType.Lake,
                            urlBase + "Lake.png"
                        },
                        {
                            TerrainType.Mountains,
                            urlBase + "Mountains.png"
                        },
                        {
                            TerrainType.Ocean,
                            urlBase + "Ocean.png"
                        },
                        {
                            TerrainType.Stone,
                            urlBase + "Stone.png"
                        },
                        {
                            TerrainType.Wood,
                            urlBase + "Wood.png"
                        }
                    };
                }

                //return _terrainTextureMaping;
                return new Dictionary<TerrainType, string>();
            }
        }

        public static Dictionary<BuildingType, string> BuildingTextureMaping
        {
            get
            {
                if(_buildingTextureMaping == null)
                {
                    var urlBase = "~/Content/Building/";

                    _buildingTextureMaping = new Dictionary<BuildingType, string>
                    {
                        {
                            BuildingType.Farm,
                            urlBase + "Farm.png"
                        },
                        {
                            BuildingType.House,
                            urlBase + "House.png"
                        },
                        {
                            BuildingType.Road,
                            urlBase + "Road.png"
                        },
                        {
                            BuildingType.Sawmill,
                            urlBase + "Sawmill.png"
                        },
                        {
                            BuildingType.Stonepit,
                            urlBase + "Stonepit.png"
                        },
                        {
                            BuildingType.TownHall,
                            urlBase + "TownHall.png"
                        }
                    };
                }

                //return _buildingTextureMaping;
                return new Dictionary<BuildingType, string>();
            }           
        }
    }
}