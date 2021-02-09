using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        public Dictionary<string, PalettePointTerrain> Terrains { get; }
        public Dictionary<string, BuildingCondition> BuildingConditions { get; }
        public Dictionary<string, Building> Buildings { get; }

        public PalettePoint()
        {
            Terrains = new Dictionary<string, PalettePointTerrain>();
            BuildingConditions = new Dictionary<string, BuildingCondition>();
            Buildings = new Dictionary<string, Building>();
        }
    }
}