using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models.Layers.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers
{
    public class TerrainLayer : LayerBase<TerrainLayerItem>
    {
        public override string Name { get { return "Terrain"; } }

        public bool IsHasTerrain(TerrainType terrainType)
        {
            return Items.Any(a => a.TerrainType == terrainType);
        }

        public void AddTerrain(TerrainType terrainType, int value)
        {
            var item = new TerrainLayerItem
            {
                TerrainType = terrainType,
                Value = value
            };

            Items.Add(item);
        }

        public TerrainLayerItem GetTerrain(TerrainType terrainType)
        {
            return Items.FirstOrDefault(a => a.TerrainType == terrainType);
        }

        public List<TerrainType> TerrainTypes
        {
            get
            {
                return Items.Select(a => a.TerrainType).ToList();
            }
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

        public int GetMaxTerrainValue()
        {
            return Items.Select(a => a.Value).Max();
        }
    }
}
