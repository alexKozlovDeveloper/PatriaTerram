using AStarAlgorithm.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarAlgorithm
{
    public class AStarResolver
    {
        private Node[][] _nodes;

        public int Width
        {
            get
            {
                if (_nodes == null) { return 0; }

                return _nodes.Length;
            }
        }
        public int Height
        {
            get
            {
                if (_nodes == null) { return 0; }
                if (_nodes.Length == 0) { return 0; }
                if (_nodes[0] == null) { return 0; }

                return _nodes[0].Length;
            }
        }

        public AStarResolver(int[][] matrix)
        {
            _nodes = Convert(matrix);
        }

        public IEnumerable<Coord> GetPath(Coord start, Coord finish)
        {
            var startNode = _nodes[start.X][start.Y];
            startNode.CostToStart = 0;

            var reachable = new List<Node> { startNode };
            var explored = new List<Node> { };

            while (reachable.Any()) 
            {
                var node = ChooseNode(reachable);

                if(node.Coord.Equals(finish))
                {
                    return BuildPath(node);
                }

                reachable.Remove(node);
                explored.Add(node);

                var newReachable = GetAdjacentNodes(node).Except(explored);


                foreach (var item in newReachable)
                {
                    if(reachable.Contains(item) == false)
                    {
                        item.Previous = node;
                        item.CostToStart = node.CostToStart + item.NodeCost;
                        reachable.Add(item);
                    }
                    else
                    {
                        if(item.CostToStart > node.CostToStart + item.NodeCost)
                        {
                            item.Previous = node;
                            item.CostToStart = node.CostToStart + item.NodeCost;
                        }
                    }
                }
            }

            return null;
        }

        private Node ChooseNode(List<Node> reachable)
        {
            var items = reachable.OrderBy(a => a.CostToStart);

            return items.First();
        }

        private Node[][] Convert(int[][] matrix)
        {
            var nodes = new Node[matrix.Length][];

            for (int x = 0; x < matrix.Length; x++)
            {
                nodes[x] = new Node[matrix[x].Length];

                for (int y = 0; y < matrix[x].Length; y++)
                {
                    nodes[x][y] = new Node(new Coord(x, y), matrix[x][y]);
                }
            }

            return nodes;
        }

        private IEnumerable<Coord> BuildPath(Node finish)
        {
            var result = new List<Coord>();

            var node = finish;

            while(node.Previous != null)
            {
                result.Add(node.Coord);
                node = node.Previous;
            }

            result.Add(node.Coord);

            result.Reverse();

            return result;
        }

        private IEnumerable<Node> GetAdjacentNodes(Node node)
        {
            var coords = GetAdjacentCoords(node.Coord);

            var nodes = new List<Node>();

            foreach (var coord in coords)
            {
                nodes.Add(this[coord]);
            }

            return nodes;
        }

        private IEnumerable<Coord> GetAdjacentCoords(Coord coord)
        {
            var coords = new List<Coord>();

            coords.Add(new Coord
            {
                X = GetXInRange(coord.X - 1, Width),
                Y = coord.Y
            });

            coords.Add(new Coord
            {
                X = GetXInRange(coord.X + 1, Width),
                Y = coord.Y
            });

            coords.Add(new Coord
            {
                X = coord.X,
                Y = GetXInRange(coord.Y + 1, Height)
            });

            coords.Add(new Coord
            {
                X = coord.X,
                Y = GetXInRange(coord.Y - 1, Height)
            });

            return coords;
        }

        private int GetXInRange(int x, int size)
        {
            if(x >= 0 && x < size)
            {
                return x;
            }
            else if(x < 0)
            {
                while(x < 0)
                {
                    x += size;
                }

                return x;
            }
            else
            {
                while (x >= size)
                {
                    x -= size;
                }

                return x;
            }
        }

        private Node this[Coord coord]
        {
            get 
            { 
                return _nodes[coord.X][coord.Y]; 
            }
        }
    }
}
