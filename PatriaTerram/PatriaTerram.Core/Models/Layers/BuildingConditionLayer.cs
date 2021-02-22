using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models.Layers.Entityes;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models.Layers
{
    public class BuildingConditionLayer : LayerBase<BuildingConditionLayerItem>
    {
        public override string Name { get { return "BuildingCondition"; } }

        public bool IsHasCondition(string townName, BuildingType buildingType, BuildingType environmentBuildingType)
        {
            return Items.Any(a => a.TownName == townName 
                               && a.BuildingType == buildingType 
                               && a.EnvironmentBuildingType == environmentBuildingType);
        }

        public List<BuildingConditionLayerItem> GetCondiotions(string townName, BuildingType buildingType)
        {
            return Items.Where(a => a.TownName == townName 
                                 && a.BuildingType == buildingType).ToList();
        }

        public int GetMaxValue()
        {
            if(Items.Count == 0)
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
            var items = Items.Where(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);

            if (items.Any() == false)
            {
                return 0;
            }

            return (int)items.Select(a => a.Value).Average();
        }

        public void UpdateValue(string townName, BuildingType buildingType, BuildingType environmentBuildingType, int value)
        {
            var item = GetItem(townName, buildingType, environmentBuildingType);

            if (item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentBuildingType = environmentBuildingType
                };

                Items.Add(item);
            }

            item.Value = value;
        }

        public void AddConditionValue(string townName, BuildingType buildingType, BuildingType environmentBuildingType, int value)
        {
            var item = GetItem(townName, buildingType, environmentBuildingType);

            if(item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    EnvironmentBuildingType = environmentBuildingType,
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

        public BuildingConditionLayerItem GetItem(string townName, BuildingType buildingType, BuildingType environmentBuildingType)
        {
            return Items.FirstOrDefault(a => a.TownName == townName 
                                          && a.BuildingType == buildingType 
                                          && a.EnvironmentBuildingType == environmentBuildingType);
        }
    }
}
