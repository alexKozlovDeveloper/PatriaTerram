using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        public Dictionary<Terrain, int> Terrains { get; }
        public List<BuildingCondition> BuildingConditions { get; }
        public List<Building> Buildings { get; }

        public PalettePoint()
        {
            Terrains = new Dictionary<Terrain, int>();
            BuildingConditions = new List<BuildingCondition>();
            Buildings = new List<Building>();
        }
    }
}