using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using PatriaTerram.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class PaletteContext
    {
        public int MaxTerrainValue { get; set; }
        public PaletteStatistics PaletteStatistics { get; set; }
        public IEnumerable<string> TownNames { get; set; }

        public Dictionary<string, Dictionary<string, Range>> TownConditionRanges { get; set; }
        public Dictionary<string, string> Layers { get; set; }

        public Dictionary<TerrainType, string> TerrainTextureMaping { get; set; }
        public Dictionary<BuildingType, string> BuildingTextureMaping { get; set; }

        public PaletteContext(Palette<ConditionPalettePoint> palette)
        {
            Layers = new Dictionary<string, string>();
            TownNames = new List<string>();
            TownConditionRanges = new Dictionary<string, Dictionary<string, Range>>();

            PaletteStatistics = palette.Statistics;
            TownNames = PaletteStatistics.GetAllTownNames();
            MaxTerrainValue = PaletteStatistics.GetMaxTerrainValue();

            TerrainTextureMaping = ImageConfigs.TerrainTextureMaping;
            BuildingTextureMaping = ImageConfigs.BuildingTextureMaping;

            foreach (var building in ConditionConfigs.Buildings.Values)
            {
                TownConditionRanges.Add(building.Type.ToString(), palette.GetConditionRanges(TownNames, building));
            }            
        }

        public void AddLayer(string layer, string classes)
        {
            if(Layers.Keys.Contains(layer) == false)
            {
                Layers.Add(layer, classes);
            }
        }
    }
}