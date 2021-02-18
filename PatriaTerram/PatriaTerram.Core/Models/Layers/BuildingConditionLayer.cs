﻿using PatriaTerram.Core.Enums;
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

        public BuildingConditionLayerItem GetCondiotion(BuildingType buildingType)
        {
            return Items.FirstOrDefault(a => a.BuildingType == buildingType);
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

        public void AddConditionValue(BuildingType buildingType, string environment, int value)
        {
            var item = new BuildingConditionLayerItem
            {
                BuildingType = buildingType,
                Environment = environment,
                Value = value
            };

            Items.Add(item);
        }
    }
}