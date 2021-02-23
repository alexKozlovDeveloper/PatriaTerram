using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Layers.Entityes;
using PatriaTerram.Core.Layers;
using System.Linq;

namespace PatriaTerram.Core.Condition.Layers
{
    public class ResultConditionLayer : LayerBase<ResultConditionLayerItem>
    {
        public override string Name { get { return "ResultCondition"; } }

        public void UpdateValue(string townName, BuildingType buildingType, int value)
        {
            var item = GetItem(townName, buildingType);

            if (item == null)
            {
                item = new ResultConditionLayerItem
                {
                    BuildingType = buildingType,
                    Value = value,
                    TownName = townName
                };

                Items.Add(item);
            }

            item.Value = value;
        }

        public int GetValue(string townName, BuildingType buildingType)
        {
            var item = GetItem(townName, buildingType);

            if (item == null)
            {
                return 0;
            }

            return item.Value;
        }

        public bool IsHasCondition(string townName, BuildingType buildingType)
        {
            return Items.Any(a => a.TownName == townName && a.BuildingType == buildingType);
        }

        public ResultConditionLayerItem GetItem(string townName, BuildingType buildingType)
        {
            return Items.FirstOrDefault(a => a.TownName == townName && a.BuildingType == buildingType);
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
    }
}
