using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class Palette<Point> where Point : TerrainPalettePoint
    {
        public PaletteStatistics Statistics { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        private Point[][] _points;

        private IPalettePointFactory<Point> _pointFactory;

        public Palette(int width, int height, IPalettePointFactory<Point> pointFactory)
        {
            Width = width;
            Height = height;

            _pointFactory = pointFactory;

            Statistics = new PaletteStatistics();

            CreateEmptyPoints();
        }

        private void CreateEmptyPoints()
        {
            _points = new Point[Width][];

            for (int i = 0; i < Width; i++)
            {
                _points[i] = new Point[Height];

                for (int j = 0; j < Height; j++)
                {
                    var point = _pointFactory.Create(i, j);

                    foreach (var layer in point.GetLayers())
                    {
                        layer.AddItemEvent += Statistics.AddLayerValue;
                    }

                    _points[i][j] = point;
                }
            }
        }

        public Point this[int i, int j]
        {
            get { return _points[i][j]; }
            set { _points[i][j] = value; }
        }

        public Point this[Coord coord]
        {
            get
            { 
                if(coord == null)
                {
                    return default;
                }

                return _points[coord.X][coord.Y]; 
            }
            set { _points[coord.X][coord.Y] = value; }
        }

        public IEnumerable<Point> AllPoints
        {
            get
            {
                var items = new List<Point>();

                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        items.Add(this[x, y]);
                    }
                }

                return items;
            }
        }
    }
}