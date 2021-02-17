using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarAlgorithm.Entityes
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord()
        {

        }

        public int CompareTo(object obj)
        {
            var item = obj as Coord;

            if (item.X == X && item.Y == Y)
            {
                return 0;
            }

            return 1;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Coord;

            return item.X == X && item.Y == Y;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
