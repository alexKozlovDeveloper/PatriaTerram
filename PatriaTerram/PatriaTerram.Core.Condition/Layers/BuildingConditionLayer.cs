using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Layers.Entityes;
using PatriaTerram.Core.Layers;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Layers
{
    public class BuildingConditionLayer : LayerBase<BuildingConditionLayerItem>
    {
        public override string Name { get { return "BuildingCondition"; } }

        public bool IsHasCondition(string townName, BuildingType buildingType, BuildingType environmentBuildingType)
        {
            return GetAll().Any(a => a.TownName == townName 
                               && a.BuildingType == buildingType 
                               && a.EnvironmentBuildingType == environmentBuildingType);
        }

        public List<BuildingConditionLayerItem> GetCondiotions(string townName, BuildingType buildingType)
        {
            return GetAll().Where(a => a.TownName == townName 
                                 && a.BuildingType == buildingType).ToList();
        }

        public int GetValue(string townName, BuildingType buildingType, BuildingType environmentBuildingType)
        {
            var item = GetItem(townName, buildingType, environmentBuildingType);

            if (item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public int GetValue(BuildingType buildingType, BuildingType environmentBuildingType)
        {
            var items = GetAll().Where(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);

            if (items.Any() == false)
            {
                return 0;
            }

            return (int)items.Select(a => a.Value).Average();
        }

        public void UpdateValue(string townName, BuildingType buildingType, BuildingType environmentBuildingType, int value, BuildingCondition condition)
        {
            var item = GetItem(townName, buildingType, environmentBuildingType);

            if (item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentBuildingType = environmentBuildingType,
                    Condition = condition
                };

                AddItem(item);
            }

            item.Value = value;

            ThrowUpdateItemValueEvent(item.Descriptor, item.Value);
        }

        public void AddConditionValue(string townName, BuildingType buildingType, BuildingType environmentBuildingType, int value, BuildingCondition condition)
        {
            var item = GetItem(townName, buildingType, environmentBuildingType);

            if(item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentBuildingType = environmentBuildingType,
                    Value = value,
                    TownName = townName,
                    Condition = condition
                };

                AddItem(item);
            }
            else
            {
                item.Value += value;

                ThrowUpdateItemValueEvent(item.Descriptor, item.Value);
            }
        }

        public BuildingConditionLayerItem GetItem(string townName, BuildingType buildingType, BuildingType environmentBuildingType)
        {
            return GetAll().FirstOrDefault(a => a.TownName == townName 
                                          && a.BuildingType == buildingType 
                                          && a.EnvironmentBuildingType == environmentBuildingType);
        }
    }
}
