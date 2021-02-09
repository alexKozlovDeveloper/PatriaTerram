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
        public Dictionary<string, string> Layers { get; set; }

        public PaletteContext()
        {
            MaxConditions = new Dictionary<string, int>();
            Layers = new Dictionary<string, string>();
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