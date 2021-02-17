using System;
using AStarAlgorithm.Entityes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AStarAlgorithm.Test
{
    [TestClass]
    public class AStarAlgorithmTest
    {
        [TestMethod]
        public void GetPath()
        {
            var matrix = GetMatrix();

            var start = new Coord(0, 0);
            var finish = new Coord(2, 2);

            var resolver = new AStarResolver(matrix);

            var path = resolver.GetPath(start, finish);

        }

        private int[][] GetMatrix()
        {
            var width = 5;
            var height = 5;

            int[][] matrix = new int[width][];

            for (int i = 0; i < height; i++)
            {
                matrix[i] = new int[height];
            }

            matrix[0][0] = 1;
            matrix[0][1] = 1;
            matrix[0][2] = 1;
            matrix[0][3] = 1;
            matrix[0][4] = 1;

            matrix[1][0] = 1;
            matrix[1][1] = 999;
            matrix[1][2] = 999;
            matrix[1][3] = 999;
            matrix[1][4] = 1;

            matrix[2][0] = 1;
            matrix[2][1] = 999;
            matrix[2][2] = 1;
            matrix[2][3] = 999;
            matrix[2][4] = 1;

            matrix[3][0] = 1;
            matrix[3][1] = 999;
            matrix[3][2] = 1;
            matrix[3][3] = 999;
            matrix[3][4] = 1;

            matrix[4][0] = 1;
            matrix[4][1] = 1;
            matrix[4][2] = 1;
            matrix[4][3] = 1;
            matrix[4][4] = 1;

            return matrix;
        }
    }
}
