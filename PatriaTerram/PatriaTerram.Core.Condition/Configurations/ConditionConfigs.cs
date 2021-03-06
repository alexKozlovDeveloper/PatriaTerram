﻿using Newtonsoft.Json;
using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using System.Collections.Generic;
using System.IO;

namespace PatriaTerram.Core.Condition.Configurations
{
    public static class ConditionConfigs
    {
        public static string TerrainsJsonFilePath = @"Configurations\Files\Buildings.json";

        private static Dictionary<string, Dictionary<BuildingType, Building>> _buildings;
        private static Dictionary<TerrainType, int> _terrainPassability;
        private static Dictionary<BuildingType, int> _buildingPassability;

        public static Dictionary<BuildingType, Building> Buildings
        {
            get
            {
                if (_buildings == null)
                {
                    var json = File.ReadAllText(TerrainsJsonFilePath);
                    _buildings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<BuildingType, Building>>>(json);
                }

                //return _buildings["test"]; 
                return _buildings["main"]; 
            }
        }

        public static Dictionary<TerrainType, int> TerrainPassability
        {
            get
            {
                if (_terrainPassability == null)
                {
                    _terrainPassability = new Dictionary<TerrainType, int>
                    {
                        {
                            TerrainType.Beach,
                            15
                        },
                        {
                            TerrainType.FertileSoil,
                            8
                        },
                        {
                            TerrainType.Ground,
                            7
                        },
                        {
                            TerrainType.Lake,
                            1000
                        },
                        {
                            TerrainType.Mountains,
                            1000
                        },
                        {
                            TerrainType.Ocean,
                            1000
                        },
                        {
                            TerrainType.Stone,
                            25
                        },
                        {
                            TerrainType.Wood,
                            10
                        }
                    };
                }

                return _terrainPassability;
            }
        }

        public static Dictionary<BuildingType, int> BuildingPassability
        {
            get
            {
                if (_buildingPassability == null)
                {
                    _buildingPassability = new Dictionary<BuildingType, int>
                    {
                        {
                            BuildingType.Farm,
                            5
                        },
                        {
                            BuildingType.FishermanHouse,
                            5
                        },
                        {
                            BuildingType.House,
                            5
                        },
                        {
                            BuildingType.Market,
                            5
                        },
                        {
                            BuildingType.Sawmill,
                            5
                        },
                        {
                            BuildingType.Stonepit,
                            5
                        },
                        {
                            BuildingType.TownHall,
                            5
                        },
                        {
                            BuildingType.Warehouse,
                            5
                        },
                        {
                            BuildingType.Road,
                            5
                        },
                        {
                            BuildingType.Mill,
                            5
                        },
                        {
                            BuildingType.StonepitTownHall,
                            5
                        },
                        {
                            BuildingType.FarmTownHall,
                            5
                        }
                    };
                }

                return _buildingPassability;
            }
        }

        /// <summary>
        /// Legacy
        /// </summary>
        private static void GetBuildings()
        {
            var buildings = new Dictionary<BuildingType, Building>
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
    }
}
