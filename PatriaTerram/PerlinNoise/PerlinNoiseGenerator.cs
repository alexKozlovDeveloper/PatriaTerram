using System;
using System.Collections.Generic;
using System.Linq;

namespace PerlinNoise
{
    public class PerlinNoiseGenerator
    {
        private readonly Random _random;

        private readonly int _maxValue;

        public PerlinNoiseGenerator(int seed, int maxValue)
        {
            _random = new Random(seed);

            _maxValue = maxValue;
        }

        public int[][] GetPerlinNoiseMatrix(int width, int height)
        {
            var matrixes = new List<int[][]>();

            for (int i = 2; i < width; i *= 2)
            {
                var m = GetRandomMatrix(i, i);

                m = m.IncreaseOctave(width / i);

                matrixes.Add(m);
            }

            var sum = matrixes.Average();

            return sum.Smoothing().Smoothing();
        }

        private int[][] GetPerlinNoiseMatrix_experimental(int width, int height)
        {
            var matrixes = new List<int[][]>();

            var origin = GetRandomMatrix(width, height);

            matrixes.Add(origin);

            for (int i = width / 2; i >= 2; i /= 2)
            {
                var m = origin.DecreaseOctave(i);
                var m2 = m.IncreaseOctave(i);

                matrixes.Add(m2);
            }

            var sum = matrixes.Average();

            return sum.Smoothing().Smoothing();
        }

        public int[][] GetRandomMatrix(int width, int height)
        {
            var matrix = new int[width][];

            for (int i = 0; i < width; i++)
            {
                matrix[i] = new int[height];
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    matrix[x][y] = _random.Next(0, _maxValue);
                }
            }

            return matrix;
        }         
    }
}
