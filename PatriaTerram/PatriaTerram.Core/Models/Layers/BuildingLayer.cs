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

        public void AddBuilding(BuildingType buildingType)
        {
            var item = new BuildingLayerItem
            {
                BuildingType = buildingType
            };

            Items.Add(item);
        }

        public bool IsHasAnyBuildings()
        {
            return Items.Count != 0;
        }

        public List<BuildingType> GetBuildings() 
        {
            return Items.Select(a => a.BuildingType).ToList();
        }
    }
}
