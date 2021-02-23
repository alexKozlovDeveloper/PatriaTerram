using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System.Collections.Generic;

namespace PatriaTerram.Web.Models
{
    public class PalettePointView
    {
        public List<MapCellItem> Cells { get; set; }

        public List<string> Classes { get; set; }

        public PalettePointView(ConditionPalettePoint point, PaletteContext context)
        {
            Cells = new List<MapCellItem>();
            Classes = new List<string>();

            foreach (var terrainType in point.Terrains.GetTerrainTypes())
            {
                var itemValue = point.Terrains.GetTerrainValue(terrainType);
                var terrain = Configs.Terrains[terrainType];
                var terrainColorValue = (itemValue / (double)context.MaxTerrainValue);

                var color = new Color
                {
                    R = (int)(terrain.Color.R * terrainColorValue),
                    G = (int)(terrain.Color.G * terrainColorValue),
                    B = (int)(terrain.Color.B * terrainColorValue)
                };

                if(terrainType == TerrainType.Result)
                {
                    var resultColor = point.GetPointColor();

                    color = new Color
                    {
                        R = (int)(resultColor.R * terrainColorValue),
                        G = (int)(resultColor.G * terrainColorValue),
                        B = (int)(resultColor.B * terrainColorValue)
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

            foreach (var terrainCondition in point.TerrainConditions.GetAll())
            {
                var env = terrainCondition.EnvironmentTerrainType;
                var type = terrainCondition.BuildingType;
                var value = terrainCondition.Value;
                var terrainConditionColorValue = value / (double)context.MaxConditions[$"{type}-{env}"] * 255;

                if (value == 0) { continue; }

                var cell = new MapCellItem()
                {
                    Value = value,
                    Color = new Color
                    {
                        R = (int)terrainConditionColorValue,
                        G = 0,
                        B = 0
                    },
                    Classes = new List<string> { $"{type}-{env}" }
                };

                Cells.Add(cell);

                context.AddLayer($"{type}-{env}", $".{type}-{env}");
            }

            foreach (var buildingCondition in point.BuildingConditions.GetAll())
            {
                var env = buildingCondition.EnvironmentBuildingType;
                var type = buildingCondition.BuildingType;
                var value = buildingCondition.Value;
                var town = buildingCondition.TownName;
                var buildingConditionColorValue = (value / (double)context.MaxConditions[$"{type}-{env}"]) * 255;

                if (value == 0) { continue; }

                var cell = new MapCellItem()
                {
                    Value = value,
                    Color = new Color
                    {
                        R = (int)buildingConditionColorValue,
                        G = 0,
                        B = 0
                    },
                    Classes = new List<string> { $"{type}-{env}", town }
                };

                Cells.Add(cell);

                context.AddLayer($"{type}-{env} [{town}]", $".{type}-{env}.{town}");
            }

            foreach (var resultCondition in point.ResultConditions.GetAll())
            {
                var env = "Result";
                var type = resultCondition.BuildingType;
                var value = resultCondition.Value;
                var town = resultCondition.TownName;
                var resultConditionColorResult = (value / (double)context.MaxConditions[$"{type}-{env}"]) * 255;

                if (value == 0) { continue; }

                var cell = new MapCellItem()
                {
                    Value = value,
                    Color = new Color
                    {
                        R = (int)resultConditionColorResult,
                        G = 0,
                        B = 0
                    },
                    Classes = new List<string> { $"{type}-{env}", town }
                };

                Cells.Add(cell);

                context.AddLayer($"{type}-{env} [{town}]", $".{type}-{env}.{town}");
            }

            foreach (var buildingLayerItem in point.Buildings.GetAll())
            {
                var building = ConditionConfigs.Buildings[buildingLayerItem.BuildingType];
                var town = buildingLayerItem.TownName;

                var cell = new MapCellItem()
                {
                    Value = building.Value,
                    Color = building.Color,
                    Classes = new List<string> { building.Type.ToString(), "building", buildingLayerItem.TownName }
                };

                Cells.Add(cell);

                context.AddLayer($"{building.Type} [{town}]", $".{building.Type}.{town}");
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