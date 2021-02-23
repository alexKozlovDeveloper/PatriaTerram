using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class PaletteContext
    {
        public int MaxTerrainValue { get; set; }
        public Dictionary<string, int> MaxConditions { get; set; }
        public Dictionary<string, Dictionary<string, Range>> TownConditionRanges { get; set; }
        public Dictionary<string, string> Layers { get; set; }
        public List<string> TownNames { get; set; }
        public Dictionary<TerrainType, string> TerrainTextureMaping { get; set; }
        public Dictionary<BuildingType, string> BuildingTextureMaping { get; set; }

        public PaletteContext()
        {
            MaxConditions = new Dictionary<string, int>();
            Layers = new Dictionary<string, string>();
            TownNames = new List<string>();
            TownConditionRanges = new Dictionary<string, Dictionary<string, Range>>();
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