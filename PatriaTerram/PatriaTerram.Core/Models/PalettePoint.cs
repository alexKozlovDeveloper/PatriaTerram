using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        private List<ILayer> _layers;

        public PalettePoint()
        {
            _layers = new List<ILayer>();

            _layers.Add(new TerrainLayer());
            _layers.Add(new BuildingConditionLayer());
            _layers.Add(new BuildingLayer());
        }

        public T GetLayer<T>(string layerName) where T : ILayer
        {
            var layer = _layers.FirstOrDefault(a => a.Name == layerName);

            if (layer == null)
            {
                return default;
            }

            return (T)layer;
        }

        public TerrainLayer Terrains => GetLayer<TerrainLayer>("Terrain");
        public BuildingConditionLayer BuildingConditions => GetLayer<BuildingConditionLayer>("BuildingCondition");
        public BuildingLayer Buildings => GetLayer<BuildingLayer>("Building");
    }
}