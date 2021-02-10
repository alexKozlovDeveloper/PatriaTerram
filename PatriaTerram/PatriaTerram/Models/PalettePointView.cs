using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class PalettePointView
    {
        public List<MapCellItem> Cells { get; set; }

        public PalettePointView(PalettePoint point, PaletteContext context)
        {
            Cells = new List<MapCellItem>();

            foreach (var item in point.Terrains.Values)
            {
                var cell = new MapCellItem() 
                {
                    Value = item.Value,
                    Color = new Color
                    {
                        R = (int)(item.Terrain.Color.R * (item.Value / (double)context.MaxTerrainValue)),
                        G = (int)(item.Terrain.Color.G * (item.Value / (double)context.MaxTerrainValue)),
                        B = (int)(item.Terrain.Color.B * (item.Value / (double)context.MaxTerrainValue))
                    },
                    Classes = new List<string> { item.Terrain.Name }
                };

                Cells.Add(cell);

                context.AddLayer(item.Terrain.Name, $".{item.Terrain.Name}");
            }

            foreach (var buildingCondition in point.BuildingConditions.Values)
            {
                foreach (var item in buildingCondition.EnvironmentConditionValues)
                {
                    var cell = new MapCellItem()
                    {
                        Value = item.Value,
                        Color = new Color
                        {
                            R = (int)((item.Value / (double)context.MaxConditions[$"{buildingCondition.BuildingType}-{item.Key}"]) * 255),
                            G = 0,
                            B = 0
                        },
                        Classes = new List<string> { $"{buildingCondition.BuildingType}-{item.Key}" }
                    };

                    Cells.Add(cell);

                    context.AddLayer($"{buildingCondition.BuildingType}-{item.Key}", $".{buildingCondition.BuildingType}-{item.Key}");
                }
            }

            foreach (var building in point.Buildings.Values)
            {                
                var cell = new MapCellItem()
                {
                    Value = building.Value,
                    Color = building.Color,
                    Classes = new List<string> { building.Name }
                };

                Cells.Add(cell);

                context.AddLayer($"{building.Name}", $".{building.Name}");
            }
        }
    }
}