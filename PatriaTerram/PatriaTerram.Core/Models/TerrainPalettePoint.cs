using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Layers;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class TerrainPalettePoint
    {
        protected List<ILayer> _layers;

        public TerrainPalettePoint()
        {
            _layers = new List<ILayer>
            {
                new TerrainLayer()
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
    }
}