using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Layers.Entityes;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Layers
{
    public class TerrainLayer : LayerBase<TerrainLayerItem>
    {
        public override string Name { get { return Constants.TerrainLayer; } }

        public bool IsHasTerrain(TerrainType terrainType)
        {
            return GetAll().Any(a => a.TerrainType == terrainType);
        }

        public void AddTerrain(TerrainType terrainType, int value)
        {
            var item = new TerrainLayerItem
            {
                TerrainType = terrainType,
                Value = value
            };

            AddItem(item);
        }

        public TerrainLayerItem GetTerrain(TerrainType terrainType)
        {
            return GetAll().FirstOrDefault(a => a.TerrainType == terrainType);
        }

        public List<TerrainType> GetTerrainTypes()
        {
            return GetAll().Select(a => a.TerrainType).ToList();
        }

        public int GetTerrainValue(TerrainType terrainType)
        {
            var item = GetTerrain(terrainType);

            if(item == null)
            {
                return 0;
            }

            return item.Value;
        }
    }
}
