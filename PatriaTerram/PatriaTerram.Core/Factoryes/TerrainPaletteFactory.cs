using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
using PerlinNoise;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Factoryes
{
    public class TerrainPaletteFactory : IPaletteFactory
    {
        private int _width;
        private int _height;
        private int _oceanEdge;
        private int _mountainsEdge;

        private int _fertileSoilBottomEdge;
        private int _fertileSoilTopEdge;

        private int _woodBottomEdge;
        private int _woodTopEdge;

        private int _stoneBottomEdge;
        private int _stoneTopEdge;

        private int _lakeBottomEdge;
        private int _lakeTopEdge;

        private int _seed;
        private int _maxValue = 256;

        private int _beachSize;
        private int _smoothingSize;

        private PerlinNoiseGenerator _generator;

        public TerrainPaletteFactory(int width = 32, int height = 32, 
            int seed = 666,
            int oceanEdge = 120, 
            int mountainsEdge = 160,
            int fertileSoilBottomEdge = 122,
            int fertileSoilTopEdge = 139,
            int woodBottomEdge = 124,
            int woodTopEdge = 160,
            int stoneBottomEdge = 165,
            int stoneTopEdge = 200,
            int lakeBottomEdge = 163,
            int lakeTopEdge = 200,
            int beachSize = 5,
            int smoothingSize = 1
            )
        {
            _generator = new PerlinNoiseGenerator(seed, _maxValue);

            _width = width;
            _height = height;
            _seed = seed;
            _oceanEdge = oceanEdge;
            _mountainsEdge = mountainsEdge;

            _fertileSoilBottomEdge = fertileSoilBottomEdge;
            _fertileSoilTopEdge = fertileSoilTopEdge;

            _woodBottomEdge = woodBottomEdge;
            _woodTopEdge = woodTopEdge;

            _stoneBottomEdge = stoneBottomEdge;
            _stoneTopEdge = stoneTopEdge;

            _lakeBottomEdge = lakeBottomEdge;
            _lakeTopEdge = lakeTopEdge;

            _beachSize = beachSize;
            _smoothingSize = smoothingSize;
        }

        public Palette GetPalette()
        {
            Palette model = CreateEmptyPalette(_width, _height);

            var altitudeMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize);
            var oceanMatrix = altitudeMatrix.ClearTopValue(_oceanEdge);
            var groundMatrix = altitudeMatrix.ClearBottomValue(_oceanEdge + _beachSize);
            var mountainsMatrix = altitudeMatrix.ClearBottomValue(_mountainsEdge);

            var beachMatrix = altitudeMatrix.ClearBottomValue(_oceanEdge).ClearTopValue(_oceanEdge + _beachSize);

            var terrains = Terrain.GetTerrains();

            AddTerrain(model, altitudeMatrix, terrains[Constants.Altitude]);
            AddTerrain(model, oceanMatrix, terrains[Constants.Ocean]);
            AddTerrain(model, mountainsMatrix, terrains[Constants.Mountains]);          
            AddTerrain(model, beachMatrix, terrains[Constants.Beach]);

            var lakeMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_lakeBottomEdge)
                .ClearTopValue(_lakeTopEdge);
            AddTerrain(model, lakeMatrix, terrains[Constants.Lake]);
            var lakeMatrix2 = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_lakeBottomEdge)
                .ClearTopValue(_lakeTopEdge);
            AddTerrain(model, lakeMatrix2, terrains[Constants.Lake]);
            var lakeMatrix3 = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_lakeBottomEdge)
                .ClearTopValue(_lakeTopEdge);
            AddTerrain(model, lakeMatrix3, terrains[Constants.Lake]);

            var stoneMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_stoneBottomEdge)
                .ClearTopValue(_stoneTopEdge);
            AddTerrain(model, stoneMatrix, terrains[Constants.Stone]);
            var stoneMatrix2 = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_stoneBottomEdge)
                .ClearTopValue(_stoneTopEdge);
            AddTerrain(model, stoneMatrix2, terrains[Constants.Stone]);
            var stoneMatrix3 = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_stoneBottomEdge)
                .ClearTopValue(_stoneTopEdge);
            AddTerrain(model, stoneMatrix3, terrains[Constants.Stone]);

            AddTerrain(model, groundMatrix, terrains[Constants.Ground]);

            var fertileSoilMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_fertileSoilBottomEdge)
                .ClearTopValue(_fertileSoilTopEdge);
            AddTerrain(model, fertileSoilMatrix, terrains[Constants.FertileSoil]);

            var woodMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(_woodBottomEdge)
                .ClearTopValue(_woodTopEdge);
            AddTerrain(model, woodMatrix, terrains[Constants.Wood]);

            return model;
        }

        private void AddTerrain(Palette model, int[][] terrainMatrix, Terrain terrain)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (terrainMatrix[x][y] == 0) { continue; }

                    if (model[x, y].Components.FirstOrDefault(a => a.Terrain.Name == terrain.Name) != null) { continue; }

                    var isIntolerableTerrains = false;

                    for (int i = 0; i < terrain.IntolerableTerrains.Length; i++)
                    {
                        if(model[x, y].Components.FirstOrDefault(a => a.Terrain.Name == terrain.IntolerableTerrains[i]) != null)
                        {
                            isIntolerableTerrains = true;
                            break;
                        }
                    }

                    if(isIntolerableTerrains == false)
                    {
                        model[x, y].Components.Add(new Component { Terrain = terrain, Value = terrainMatrix[x][y] });
                    }                    
                }
            }
        }

        private Palette CreateEmptyPalette(int width, int height)
        {
            var model = new Palette
            {
                Points = new PalettePoint[width][]
            };

            for (int i = 0; i < width; i++)
            {
                model.Points[i] = new PalettePoint[height];

                for (int j = 0; j < height; j++)
                {
                    model[i, j] = new PalettePoint();
                }
            }

            return model;
        }        
    }
}
