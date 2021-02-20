using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models.Layers.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers
{
    public class TerrainConditionLayer : LayerBase<TerrainConditionLayerItem>
    {
        public override string Name { get { return "TerrainCondition"; } }

        public List<TerrainConditionLayerItem> GetTerrainConditions()
        {
            return Items;
        }

        public void AddConditionValue(BuildingType buildingType, TerrainType environmentTerrain, int value, string townName = null)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentTerrainType == environmentTerrain);

            if (item == null)
            {
                item = new TerrainConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentTerrainType = environmentTerrain,
                    Value = value,
                    TownName = townName
                };

                Items.Add(item);
            }
            else
            {
                item.Value += value;
            }
        }

        public bool IsHasEnvironment(BuildingType buildingType, TerrainType environment)
        {
            return Items.Any(a => a.BuildingType == buildingType && a.EnvironmentTerrainType == environment);
        }

        public int GetValue(BuildingType buildingType, TerrainType terrainType)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentTerrainType == terrainType);

            if (item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public List<TerrainConditionLayerItem> GetAllCondiotions()
        {
            return Items;
        }
    }
}
