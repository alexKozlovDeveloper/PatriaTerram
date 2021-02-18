using AStarAlgorithm.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class Palette
    {
        private  PalettePoint[][] _points;

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

        public Palette(PalettePoint[][] points)
        {
            _points = points;
        }

        public PalettePoint this[int i, int j]
        {
            get { return _points[i][j]; }
            set { _points[i][j] = value; }
        }

        public PalettePoint this[Coord coord]
        {
            get
            { 
                if(coord == null)
                {
                    return null;
                }

                return _points[coord.X][coord.Y]; 
            }
            set { _points[coord.X][coord.Y] = value; }
        }
    }
}