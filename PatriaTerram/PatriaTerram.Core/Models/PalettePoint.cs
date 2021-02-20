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
        private readonly List<ILayer> _layers;

        public PalettePoint()
        {
            _layers = new List<ILayer>
            {
                new TerrainLayer(),
                new TerrainConditionLayer(),
                new BuildingConditionLayer(),                
                new ResultConditionLayer(),
                new BuildingLayer()
            };
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
        public TerrainConditionLayer TerrainConditions => GetLayer<TerrainConditionLayer>("TerrainCondition");
        public BuildingConditionLayer BuildingConditions => GetLayer<BuildingConditionLayer>("BuildingCondition");        
        public ResultConditionLayer ResultConditions => GetLayer<ResultConditionLayer>("ResultCondition");
        public BuildingLayer Buildings => GetLayer<BuildingLayer>("Building");
    }
}