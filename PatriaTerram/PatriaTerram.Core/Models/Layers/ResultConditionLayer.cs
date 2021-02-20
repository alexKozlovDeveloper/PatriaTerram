using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models.Layers.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers
{
    public class ResultConditionLayer : LayerBase<ResultConditionLayerItem>
    {
        public override string Name { get { return "ResultCondition"; } }

        public void UpdateValue(BuildingType buildingType, int value)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType);

            if (item == null)
            {
                item = new ResultConditionLayerItem
                {
                    BuildingType = buildingType,
                    Value = value
                };

                Items.Add(item);
            }

            item.Value = value;
        }

        public int GetValue(BuildingType buildingType)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType);

            if(item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public bool IsHasCondition(BuildingType buildingType)
        {
            return Items.Any(a => a.BuildingType == buildingType);
        }
    }
}
