using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Layers.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Layers;
using System.Linq;

namespace PatriaTerram.Core.Condition.Layers
{
    public class TerrainConditionLayer : LayerBase<TerrainConditionLayerItem>
    {
        public override string Name { get { return "TerrainCondition"; } }

        public void AddConditionValue(BuildingType buildingType, TerrainType environmentTerrain, int value, TerrainCondition condition)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentTerrainType == environmentTerrain);

            if (item == null)
            {
                item = new TerrainConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentTerrainType = environmentTerrain,
                    Value = value,
                    Condition = condition
                };

                Items.Add(item);
            }
            else
            {
                item.Value += value;
            }
        }

        public bool IsHasCondition(BuildingType buildingType, TerrainType environment)
        {
            return Items.Any(a => a.BuildingType == buildingType && a.EnvironmentTerrainType == environment);
        }

        public int GetMaxValue()
        {
            if (Items.Count == 0)
            {
                return 0;
            }

            return Items.Select(a => a.Value).Max();
        }

        public int GetMinValue()
        {
            if (Items.Count == 0)
            {
                return 0;
            }

            return Items.Select(a => a.Value).Min();
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
    }
}
