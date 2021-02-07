using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Helpers;
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
        private int _seed;
        private int _maxAltitudeValue;

        private int _oceanEdge;
        private int _mountainsEdge;
        private int _beachSize;
        private int _smoothingSize;

        private Range _fertileSoilRange;
        private Range _woodRange;
        private Range _stoneRange;
        private Range _lakeRange;

        private PerlinNoiseGenerator _generator;

        public TerrainPaletteFactory(PaletteConfiguration config)
        {           

            _width = config.Width;
            _height = config.Height;
            _seed = config.Seed;
            _oceanEdge = config.OceanEdge;
            _mountainsEdge = config.MountainsEdge;

            _fertileSoilRange = config.FertileSoilRange;
            _woodRange = config.WoodRange;
            _stoneRange = config.StoneRange;
            _lakeRange = config.LakeRange;

            _beachSize = config.BeachSize;
            _smoothingSize = config.SmoothingSize;
            _maxAltitudeValue = config.MaxAltitudeValue;

            _generator = new PerlinNoiseGenerator(_seed, _maxAltitudeValue);
        }

        public Palette GetPalette()
        {
            Palette model = CreateEmptyPalette(_width, _height);
            var terrains = Configs.Terrains;

            var altitudeMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize);
            var oceanMatrix = altitudeMatrix.ClearTopValue(_oceanEdge);
            var groundMatrix = altitudeMatrix.ClearBottomValue(_oceanEdge + _beachSize);
            var mountainsMatrix = altitudeMatrix.ClearBottomValue(_mountainsEdge);

            var beachMatrix = altitudeMatrix.ClearBottomValue(_oceanEdge).ClearTopValue(_oceanEdge + _beachSize);            

            AddTerrain(model, altitudeMatrix, terrains[Constants.Altitude]);
            AddTerrain(model, oceanMatrix, terrains[Constants.Ocean]);
            AddTerrain(model, mountainsMatrix, terrains[Constants.Mountains]);          
            AddTerrain(model, beachMatrix, terrains[Constants.Beach]);

            AddRangedTerrain(model, terrains[Constants.Lake], _lakeRange);
            AddRangedTerrain(model, terrains[Constants.Lake], _lakeRange);
            AddRangedTerrain(model, terrains[Constants.Lake], _lakeRange);

            AddRangedTerrain(model, terrains[Constants.Stone], _stoneRange);
            AddRangedTerrain(model, terrains[Constants.Stone], _stoneRange);

            AddTerrain(model, groundMatrix, terrains[Constants.Ground]);

            AddRangedTerrain(model, terrains[Constants.FertileSoil], _fertileSoilRange);
            AddRangedTerrain(model, terrains[Constants.Wood], _woodRange);

            return model;
        }

        private void AddRangedTerrain(Palette model, Terrain terrain, Range range)
        {
            var terrainMatrix = _generator.GetPerlinNoiseMatrix(_width, _smoothingSize)
                .ClearBottomValue(range.Bottom)
                .ClearTopValue(range.Top);
            AddTerrain(model, terrainMatrix, terrain);
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
