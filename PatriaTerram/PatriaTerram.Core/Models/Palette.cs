using AStarAlgorithm.Entityes;
using System.Collections.Generic;

namespace PatriaTerram.Core.Models
{
    public class Palette<Point>
    {
        private Point[][] _points;

        public int Width
        {
            get
            {
                if (_points == null) { return 0; }

                return _points.Length;
            }
        }
        public int Height
        {
            get
            {
                if (_points == null) { return 0; }
                if (_points.Length == 0) { return 0; }
                if (_points[0] == null) { return 0; }

                return _points[0].Length;
            }
        }

        public Palette(Point[][] points)
        {
            _points = points;
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