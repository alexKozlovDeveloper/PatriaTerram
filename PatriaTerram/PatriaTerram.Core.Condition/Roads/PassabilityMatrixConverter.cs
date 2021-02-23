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

        public PassabilityMatrixConverter(Dictionary<TerrainType, int> terrainPassability, Dictionary<BuildingType, int> buildingPassability)
        {
            _terrainPassability = terrainPassability;
            _buildingPassability = buildingPassability;
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
