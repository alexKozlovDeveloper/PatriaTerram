using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Helpers;
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

        public List<string> Classes { get; set; }

        public PalettePointView(PalettePoint point, PaletteContext context)
        {
            Cells = new List<MapCellItem>();
            Classes = new List<string>();

            foreach (var terrainType in point.Terrains.TerrainTypes)
            {
                var itemValue = point.Terrains.GetTerrainValue(terrainType);
                var terrain = Configs.Terrains[terrainType];

                var color = new Color
                {
                    R = (int)(terrain.Color.R * (itemValue / (double)context.MaxTerrainValue)),
                    G = (int)(terrain.Color.G * (itemValue / (double)context.MaxTerrainValue)),
                    B = (int)(terrain.Color.B * (itemValue / (double)context.MaxTerrainValue))
                };

                if(terrainType == Core.Enums.TerrainType.Result)
                {
                    var resultColor = point.GetPointColor();

                    color = new Color
                    {
                        R = (int)(resultColor.R * (itemValue / (double)context.MaxTerrainValue)),
                        G = (int)(resultColor.G * (itemValue / (double)context.MaxTerrainValue)),
                        B = (int)(resultColor.B * (itemValue / (double)context.MaxTerrainValue))
                    };
                }

                var cell = new MapCellItem() 
                {
                    Value = itemValue,
                    Color = color,
                    Classes = new List<string> { terrain.Type.ToString() }
                };

                Cells.Add(cell);

                context.AddLayer(terrain.Type.ToString(), $".{terrain.Type.ToString()}");
            }

            foreach (var buildingCondition in point.BuildingConditions.GetAllCondiotions())
            {
                //foreach (var item in buildingCondition.EnvironmentConditionValues)
                //{

                var env = buildingCondition.Environment;
                var type = buildingCondition.BuildingType;
                var value = buildingCondition.Value;

                var cell = new MapCellItem()
                {
                    Value = value,
                    Color = new Color
                    {
                        R = (int)((value / (double)context.MaxConditions[$"{type}-{env}"]) * 255),
                        G = 0,
                        B = 0
                    },
                    Classes = new List<string> { $"{type}-{env}" }
                };

                Cells.Add(cell);

                context.AddLayer($"{type}-{env}", $".{type}-{env}");
                //}
            }

            //foreach (var buildingCondition in point.BuildingConditions.Values)
            //{
            //    foreach (var item in buildingCondition.EnvironmentConditionValues)
            //    {
            //        var cell = new MapCellItem()
            //        {
            //            Value = item.Value,
            //            Color = new Color
            //            {
            //                R = (int)((item.Value / (double)context.MaxConditions[$"{buildingCondition.BuildingType}-{item.Key}"]) * 255),
            //                G = 0,
            //                B = 0
            //            },
            //            Classes = new List<string> { $"{buildingCondition.BuildingType}-{item.Key}" }
            //        };

            //        Cells.Add(cell);

            //        context.AddLayer($"{buildingCondition.BuildingType}-{item.Key}", $".{buildingCondition.BuildingType}-{item.Key}");
            //    }
            //}

            foreach (var buildingType in point.Buildings.GetBuildings())
            {
                var building = Configs.Buildings[buildingType];

                var cell = new MapCellItem()
                {
                    Value = building.Value,
                    Color = building.Color,
                    Classes = new List<string> { building.Type.ToString(), "building" }
                };

                Cells.Add(cell);

                context.AddLayer($"{building.Type.ToString()}", $".{building.Type.ToString()}");
            }

            if(point.Buildings.IsHasAnyBuildings() == true)
            {
                Classes.Add("point-with-building");
            }
            else
            {
                Classes.Add("point-without-building");

            }
        }
    }
}