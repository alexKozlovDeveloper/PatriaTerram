using Newtonsoft.Json;
using PatriaTerram.Core.Enums;
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
        private static Dictionary<string, Building> _buildings;

        public static Dictionary<string, Terrain> Terrains
        {
            get
            {
                if (_terrains == null)
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

        public static Dictionary<string, Building> Buildings
        {
            get
            {
                if (_buildings == null)
                {
                    _buildings = new Dictionary<string, Building>
                    {
                        {
                            Constants.TownHall,
                            new Building
                            {
                                Name = Constants.TownHall,
                                Color = new Color
                                {
                                    R = 255,
                                    G = 20,
                                    B = 147
                                },
                                EnvironmentConditions = new List<EnvironmentCondition>
                                {
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Stone,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Wood,
                                        Radius = 7,
                                        Priority = 10,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.FertileSoil,
                                        Radius = 5,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Lake,
                                        Radius = 8,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    }
                                }
                            }
                        },
                        {
                            Constants.Sawmill,
                            new Building
                            {
                                Name = Constants.Sawmill,
                                Color = new Color
                                {
                                    R = 153,
                                    G = 51,
                                    B = 255
                                },
                                EnvironmentConditions = new List<EnvironmentCondition>
                                {
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Wood,
                                        Radius = 5,
                                        Priority = 45,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.TownHall,
                                        Radius = 15,
                                        Priority = 25,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Sawmill,
                                        Radius = 3,
                                        Priority = 45,
                                        IsPositive = false,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                }
                            }
                        },
                        {
                            Constants.Stonepit,
                            new Building
                            {
                                Name = Constants.Stonepit,
                                Color = new Color
                                {
                                    R = 0,
                                    G = 0,
                                    B = 0
                                },
                                EnvironmentConditions = new List<EnvironmentCondition>
                                {
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Stone,
                                        Radius = 5,
                                        Priority = 50,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.TownHall,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Stonepit,
                                        Radius = 2,
                                        Priority = 50,
                                        IsPositive = false,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                }
                            }
                        },
                        {
                            Constants.Farm,
                            new Building
                            {
                                Name = Constants.Farm,
                                Color = new Color
                                {
                                    R = 178,
                                    G = 255,
                                    B = 102
                                },
                                EnvironmentConditions = new List<EnvironmentCondition>
                                {
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.FertileSoil,
                                        Radius = 5,
                                        Priority = 50,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.TownHall,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Sawmill,
                                        Radius = 3,
                                        Priority = 50,
                                        IsPositive = false,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Farm,
                                        Radius = 5,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    }
                                }
                            }
                        },
                        {
                            Constants.House,
                            new Building
                            {
                                Name = Constants.House,
                                Color = new Color
                                {
                                    R = 102,
                                    G = 51,
                                    B = 0
                                },
                                EnvironmentConditions = new List<EnvironmentCondition>
                                {
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Sawmill,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Stonepit,
                                        Radius = 15,
                                        Priority = 15,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.Farm,
                                        Radius = 5,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.OneLevel
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.TownHall,
                                        Radius = 15,
                                        Priority = 20,
                                        IsPositive = true,
                                        Type = EnvironmentConditionType.LinearDecrease
                                    },
                                    new EnvironmentCondition
                                    {
                                        Environment = Constants.House,
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
