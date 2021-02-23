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
        private static Dictionary<BuildingType, Building> _buildings;

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

        public static Dictionary<BuildingType, Building> Buildings
        {
            get
            {
                if (_buildings == null)
                {
                    _buildings = new Dictionary<BuildingType, Building>
                    {
                        {
                            BuildingType.TownHall,
                            new Building
                            {
                                Type = BuildingType.TownHall,
                                Color = new Color
                                {
                                    R = 255,
                                    G = 20,
                                    B = 147
                                },
                                BuildingConditions = new List<BuildingCondition> 
                                {
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.TownHall,
                                        Radius = 15,
                                        TownCondition = TownCondition.AnyTown,
                                        Priority = 30,
                                        IsPositive = false,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                },
                                TerrainConditions = new List<TerrainCondition> 
                                {
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.Stone,
                                        Radius = 15,
                                        Priority = 10,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.Wood,
                                        Radius = 7,
                                        Priority = 5,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.FertileSoil,
                                        Radius = 5,
                                        Priority = 5,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.Lake,
                                        Radius = 8,
                                        Priority = 10,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        },
                        {
                            BuildingType.Sawmill,
                            new Building
                            {
                                Type = BuildingType.Sawmill,
                                Color = new Color
                                {
                                    R = 153,
                                    G = 51,
                                    B = 255
                                },
                                BuildingConditions = new List<BuildingCondition>
                                {
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.TownHall,
                                        Radius = 15,
                                        InnerRadius = 5,
                                        Priority = 25,
                                        IsPositive = true,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.RingOneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Sawmill,
                                        Radius = 3,
                                        Priority = 45,
                                        IsPositive = false,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                },
                                TerrainConditions = new List<TerrainCondition>
                                {
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.Wood,
                                        Radius = 5,
                                        Priority = 45,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        },
                        {
                            BuildingType.Stonepit,
                            new Building
                            {
                                Type = BuildingType.Stonepit,
                                Color = new Color
                                {
                                    R = 0,
                                    G = 0,
                                    B = 0
                                },
                                BuildingConditions = new List<BuildingCondition>
                                {
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.TownHall,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Stonepit,
                                        Radius = 2,
                                        Priority = 50,
                                        IsPositive = false,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                },
                                TerrainConditions = new List<TerrainCondition>
                                {
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.Stone,
                                        Radius = 5,
                                        Priority = 50,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        },
                        {
                            BuildingType.Farm,
                            new Building
                            {
                                Type = BuildingType.Farm,
                                Color = new Color
                                {
                                    R = 240,
                                    G = 230,
                                    B = 140
                                },
                                BuildingConditions = new List<BuildingCondition>
                                {
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.TownHall,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Sawmill,
                                        Radius = 3,
                                        Priority = 50,
                                        IsPositive = false,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Farm,
                                        Radius = 5,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                },
                                TerrainConditions = new List<TerrainCondition>
                                {
                                    new TerrainCondition
                                    {
                                        EnvironmentTerrain = TerrainType.FertileSoil,
                                        Radius = 5,
                                        Priority = 50,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        },
                        {
                            BuildingType.House,
                            new Building
                            {
                                Type = BuildingType.House,
                                Color = new Color
                                {
                                    R = 102,
                                    G = 51,
                                    B = 0
                                },
                                BuildingConditions = new List<BuildingCondition>
                                {
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Sawmill,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Stonepit,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.Farm,
                                        Radius = 5,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.TownHall,
                                        Radius = 15,
                                        Priority = 20,
                                        IsPositive = true,
                                        IsRequired = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new BuildingCondition
                                    {
                                        EnvironmentBuilding = BuildingType.House,
                                        Radius = 5,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        }
                    };
                }

                return _buildings;
            }
        }
    }
}
