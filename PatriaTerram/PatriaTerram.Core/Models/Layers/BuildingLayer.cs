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

        public bool IsHasAnyBuildings()
        {
            return Items.Count != 0;
        }

        public List<BuildingType> GetBuildingTypes() 
        {
            return Items.Select(a => a.BuildingType).ToList();
        }

        public List<BuildingLayerItem> GetBuildings()
        {
            return Items;
        }
    }
}
