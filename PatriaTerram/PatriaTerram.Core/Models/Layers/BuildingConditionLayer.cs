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
    public class BuildingConditionLayer : LayerBase<BuildingConditionLayerItem>
    {
        public override string Name { get { return "BuildingCondition"; } }

        public bool IsHasBuildingCondition(BuildingType buildingType)
        {
            return Items.Any(a => a.BuildingType == buildingType);
        }

        public bool IsHasEnvironment(BuildingType buildingType, BuildingType environmentBuildingType)
        {
            return Items.Any(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);
        }

        public List<BuildingConditionLayerItem> GetCondiotions(BuildingType buildingType)
        {
            return Items.Where(a => a.BuildingType == buildingType).ToList();
        }

        public List<BuildingConditionLayerItem> GetAllCondiotions()
        {
            return Items;
        }

        public int GetValue(BuildingType buildingType, BuildingType environmentBuildingType)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);

            if (item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public void UpdateValue(BuildingType buildingType, BuildingType environmentBuildingType, int value)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);

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

        public void AddConditionValue(BuildingType buildingType, BuildingType environmentBuildingType, int value, string townName = null)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.EnvironmentBuildingType == environmentBuildingType);

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
    }
}
