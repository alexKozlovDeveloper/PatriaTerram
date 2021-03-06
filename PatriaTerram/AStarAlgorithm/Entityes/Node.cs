﻿namespace AStarAlgorithm.Entityes
{
    class Node
    {
        public Node Previous { get; set; }
        public Coord Coord { get; set; }
        public int NodeCost { get; set; }
        public int CostToStart { get; set; }

        public Node(Coord coord, int nodeCost)
        {
            Coord = coord;
            NodeCost = nodeCost;
            CostToStart = int.MaxValue;
        }
    }
}
