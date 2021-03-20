using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PatriaTerram.Core.Models
{
    public class Palette<Point> where Point : TerrainPalettePoint
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PaletteStatistics Statistics { get; private set; }

        public IEnumerable<Point> AllPoints { get { return _allPoints; } }

        private Point[][] _points;
        private List<Point> _allPoints;

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
                        layer.UpdateItemValueEvent += Statistics.UpdateLayerItemValue;
                    }

                    _points[i][j] = point;
                }
            }

            _allPoints = new List<Point>(Width * Height);
            _points.ToList().ForEach(a => _allPoints.AddRange(a));
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
    }
}