using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Condition.Roads
{
    public class PassabilityMatrixConverter
    {
        private Dictionary<TerrainType, int> _terrainPassability;
        private Dictionary<BuildingType, int> _buildingPassability;

        public PassabilityMatrixConverter()
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
                }
            };
        }

        public int[][] Convert(Palette<ConditionPalettePoint> palette)
        {
            var matrix = new int[palette.Width][];

            for (int i = 0; i < palette.Width; i++)
            {
                matrix[i] = new int[palette.Height];
            }

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    matrix[x][y] = GetPointPassability(palette[x, y]);
                }
            }

            return matrix;
        }

        private int GetPointPassability(ConditionPalettePoint point)
        {
            if(point.Buildings.IsHasAnyBuildings() == true)
            {
                return point.Buildings.GetAll()
                    .Where(a => _buildingPassability.ContainsKey(a.BuildingType))
                    .Select(a => _buildingPassability[a.BuildingType])
                    .Min();
            }
            else
            {
                return point.Terrains.GetAll()
                    .Where(a => _terrainPassability.ContainsKey(a.TerrainType))
                    .Select(a => _terrainPassability[a.TerrainType])
                    .Max();
            }
        }
    }
}
