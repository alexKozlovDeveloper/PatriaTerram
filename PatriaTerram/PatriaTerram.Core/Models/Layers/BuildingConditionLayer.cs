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

        public bool IsHasEnvironment(BuildingType buildingType, string environment)
        {
            return Items.Any(a => a.BuildingType == buildingType && a.Environment == environment);
        }

        public List<BuildingConditionLayerItem> GetCondiotions(BuildingType buildingType)
        {
            return Items.Where(a => a.BuildingType == buildingType).ToList();
        }

        public List<BuildingConditionLayerItem> GetAllCondiotions()
        {
            return Items;
        }

        public int GetValue(BuildingType buildingType, string environment)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.Environment == environment);

            if (item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public void UpdateValue(BuildingType buildingType, string environment, int value)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.Environment == environment);

            if (item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    Environment = environment
                };

                Items.Add(item);
            }

            item.Value = value;
        }

        public void AddConditionValue(BuildingType buildingType, string environment, int value)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.Environment == environment);

            if(item == null)
            {
                item = new BuildingConditionLayerItem
                {
                    BuildingType = buildingType,
                    Environment = environment,
                    Value = value
                };

                Items.Add(item);
            }
            else
            {
                item.Value += value;
            }
        }

        //public void AddBuildingConditions(string buildingType, string terrain, int value)
        //{
        //    if (BuildingConditions.Keys.Contains(buildingType) == false)
        //    {
        //        var newCondition = new BuildingCondition
        //        {
        //            BuildingType = buildingType
        //        };

        //        BuildingConditions.Add(newCondition.BuildingType, newCondition);
        //    }

        //    BuildingConditions[buildingType].AddConditionValue(terrain, value);
        //}

        //public int GetBuildingConditionValue(string buildingType, string terrain)
        //{
        //    if (BuildingConditions.Keys.Contains(buildingType) == false) { return 0; }

        //    return BuildingConditions[buildingType].EnvironmentConditionValues[terrain];
        //}
    }
}
