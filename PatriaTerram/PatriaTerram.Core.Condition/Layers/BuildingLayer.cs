using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Layers.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Layers;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Condition.Layers
{
    public class BuildingLayer : LayerBase<BuildingLayerItem>
    {
        public override string Name { get { return Constants.BuildingLayer; } }

        public void AddBuilding(BuildingType buildingType, string townName = null)
        {
            var item = new BuildingLayerItem
            {
                BuildingType = buildingType,
                TownName = townName
            };

            AddItem(item);
        }

        public void AddIfNotExist(BuildingType buildingType, string townName)
        {
            var item = GetAll().FirstOrDefault(a => a.BuildingType == buildingType && a.TownName == townName);

            if(item == null)
            {
                AddBuilding(buildingType, townName);
            }
        }

        public bool IsHasAnyBuildings()
        {
            return ItemsCount != 0;
        }

        public bool IsHasBuildings(BuildingType buildingType, string town)
        {
            return GetAll().Any(a => a.BuildingType == buildingType && a.TownName == town);
        }

        public int Count()
        {
            return ItemsCount;
        }

        public List<BuildingType> GetBuildingTypes() 
        {
            return GetAll().Select(a => a.BuildingType).ToList();
        }
    }
}
