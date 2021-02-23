using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Layers.Entityes;
using PatriaTerram.Core.Layers;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Layers
{
    public class BuildingLayer : LayerBase<BuildingLayerItem>
    {
        public override string Name { get { return "Building"; } }

        public void AddBuilding(BuildingType buildingType, string townName = null)
        {
            var item = new BuildingLayerItem
            {
                BuildingType = buildingType,
                TownName = townName
            };

            Items.Add(item);
        }

        public void AddIfNotExist(BuildingType buildingType, string townName)
        {
            var item = Items.FirstOrDefault(a => a.BuildingType == buildingType && a.TownName == townName);

            if(item == null)
            {
                AddBuilding(buildingType, townName);
            }
        }

        public bool IsHasAnyBuildings()
        {
            return Items.Count != 0;
        }

        public int Count()
        {
            return Items.Count;
        }

        public List<BuildingType> GetBuildingTypes() 
        {
            return Items.Select(a => a.BuildingType).ToList();
        }
    }
}
