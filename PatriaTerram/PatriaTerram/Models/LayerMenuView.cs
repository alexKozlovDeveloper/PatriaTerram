using PatriaTerram.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class LayerMenuView
    {
        public Dictionary<string, Dictionary<string, string>> LayersMenu { get; set; }

        public LayerMenuView(PaletteContext context)
        {
            LayersMenu = new Dictionary<string, Dictionary<string, string>>();

            AddTownLayer(LayersMenu, context);
            AddTerrainLayer(LayersMenu, context);
            AddConditionLayer(LayersMenu, context);
        }

        private void AddConditionLayer(Dictionary<string, Dictionary<string, string>> LayersMenu, PaletteContext context)
        {
            foreach (var building in Configs.Buildings.Values)
            {
                var buildingLayers = new Dictionary<string, string>();

                var keys = context.Layers.Keys.Where(a => a.StartsWith(building.Type.ToString())).OrderBy(a => a);

                foreach (var key in keys)
                {
                    buildingLayers.Add(key, context.Layers[key]);
                }

                LayersMenu.Add(building.Type.ToString(), buildingLayers);
            }
        }

        private void AddTownLayer(Dictionary<string, Dictionary<string, string>> LayersMenu, PaletteContext context)
        {
            var towns = new Dictionary<string, string>();

            foreach (var townName in context.TownNames)
            {
                towns.Add(townName, $".{townName}");
            }

            LayersMenu.Add("Towns", towns);
        }

        private void AddTerrainLayer(Dictionary<string, Dictionary<string, string>> LayersMenu, PaletteContext context)
        {
            var terrains = new Dictionary<string, string>();

            foreach (var terrain in Configs.Terrains.Values)
            {
                if (context.Layers.Keys.Contains(terrain.Type.ToString()) == true)
                {
                    terrains.Add(terrain.Type.ToString(), context.Layers[terrain.Type.ToString()]);
                }
            }

            LayersMenu.Add("Terrains", terrains);
        }
    }
}